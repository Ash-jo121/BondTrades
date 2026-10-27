using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BondsController:ControllerBase
{
    private readonly IBondRepository _repository;
    public BondsController(IBondRepository bondRepository)
    {
        _repository = bondRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Bond>>> getBondsAsync(CancellationToken cancellationToken)
    {
        List<Bond>bonds = await _repository.GetAllAsync(cancellationToken);
        return Ok(bonds);
    }

    [HttpGet("{isin}")]
    public async Task<ActionResult<Bond>> getBondsIsinAsync(string isin,CancellationToken cancellationToken)
    {
        Bond? bondsIsin = await _repository.GetByIsinAsync(isin,cancellationToken);
        if(bondsIsin == null)
        {
            return NotFound();
        }
        return Ok(bondsIsin);
    }


}