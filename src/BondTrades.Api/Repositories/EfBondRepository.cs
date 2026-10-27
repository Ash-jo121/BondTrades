using Microsoft.EntityFrameworkCore;

public class EfBondRepository : IBondRepository
{
    private readonly AppDbContext _dbContext;

    public EfBondRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Bond>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Bonds.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Bond?> GetByIsinAsync(
    string isin,
    CancellationToken cancellationToken)
    {
        return await _dbContext.Bonds
            .AsNoTracking()
            .FirstOrDefaultAsync(
                bond => bond.Isin == isin,
                cancellationToken);
    }
}