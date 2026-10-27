public class InMemoryBondRepository : IBondRepository
{
    private readonly List<Bond> _bonds = new()
    {
        new Bond
        {
            Id = 1,
            Isin = "US1234567890",
            Issuer = "Example Corp",
            Currency = "USD",
            CouponRate = 4.25m,
            MaturityDate = new DateOnly(2030, 6, 15)
        },
        new Bond
        {
            Id = 2,
            Isin = "GB1234567890",
            Issuer = "Example Bank",
            Currency = "GBP",
            CouponRate = 5.1m,
            MaturityDate = new DateOnly(2032, 3, 20)
        }
    };

    public Task<List<Bond>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = _bonds.ToList();
        return Task.FromResult(result);
    }

    public Task<Bond?> GetByIsinAsync(string isin,CancellationToken cancellationToken)
    {
        var result = _bonds.FirstOrDefault(b => b.Isin == isin);
        return Task.FromResult(result);
    }
    
}