public class EfTradeRepository : ITradeRepository
{
    private readonly AppDbContext _dbContext;
    public EfTradeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        Trade trade,
        CancellationToken cancellationToken)
    {
        await _dbContext.Trades.AddAsync(trade,cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}