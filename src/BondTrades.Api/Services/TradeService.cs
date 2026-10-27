using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;

public class TradeService : ITradeService
{
    public readonly IBondRepository _bondRepository;
    public readonly ITradeRepository _tradeRepository;
    public readonly TradingLimits _tradingLimits;
    public readonly TimeProvider _timeProvider;
    public TradeService(IBondRepository bondRepository,ITradeRepository tradeRepository,IOptions<TradingLimits> tradingLimits,TimeProvider timeProvider)
    {
        _bondRepository = bondRepository;
        _tradeRepository = tradeRepository;
        _tradingLimits = tradingLimits.Value;
        _timeProvider = timeProvider;

    }
    public async Task<Trade> BookAsync(CreateTradeRequestDto request,string? idempotencyKey,CancellationToken cancellationToken)
    {
        Bond? bond = await _bondRepository.GetByIsinAsync(request.BondIsin,cancellationToken);
        DateTime utcNow = _timeProvider.GetUtcNow().DateTime;
        if(bond != null)
        {
            if(bond.MaturityDate >= DateOnly.FromDateTime(utcNow))
            {
                TimeZoneInfo targetZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetZone);
                TimeSpan timeNow = localTime.TimeOfDay;
                TimeSpan tradingStart = new TimeSpan(9,0,0);
                TimeSpan tradingEnd = new TimeSpan(17,0,0);
                if(timeNow >= tradingStart && timeNow <= tradingEnd)
                {
                    decimal notional = request.Quantity * request.Price / 100m;
                    if(notional <= _tradingLimits.MaxNotional)
                    {
                        Trade trade = new Trade
                        {
                            Id=0,
                            BondId = bond.Id,
                            Book = request.Book,
                            CreatedAt=utcNow,
                            IdempotencyKey = idempotencyKey,
                            Price=request.Price,
                            Quantity=request.Quantity,
                            RowVersion=0,
                            Side=request.Side,
                            Status=TradeStatus.Booked,
                            TradeDate=utcNow

                        };

                        await _tradeRepository.AddAsync(trade,cancellationToken);
                        await _tradeRepository.SaveChangesAsync(cancellationToken);
                        return trade;
                    }
                    else
                    {
                        throw new BusinessRuleException("notional should be less than the tradinglimits");
                    }
                }
                else
                {
                    throw new BusinessRuleException("trading window has closed");
                }
            }
            else
            {
                throw new BusinessRuleException("Bond Maturity Date has passed");
            }
        }
        else
        {
            throw new BusinessRuleException("Bond doesnt exist");
        }
    }
}