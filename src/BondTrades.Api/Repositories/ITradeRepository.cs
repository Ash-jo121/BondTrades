public interface ITradeRepository
{
    Task AddAsync(
        Trade trade,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}