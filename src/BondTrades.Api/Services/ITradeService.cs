public interface ITradeService
{
    Task<Trade> BookAsync(
        CreateTradeRequestDto request,
        string? idempotencyKey,
        CancellationToken cancellationToken);
}