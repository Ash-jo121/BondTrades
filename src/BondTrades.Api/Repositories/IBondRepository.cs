public interface IBondRepository
{
    public Task<List<Bond>> GetAllAsync(CancellationToken cancellationToken);
    public Task<Bond?> GetByIsinAsync(string isin,CancellationToken cancellationToken);
}