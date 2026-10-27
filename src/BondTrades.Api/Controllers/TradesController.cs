using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TradesController : ControllerBase
{
    public readonly ITradeService _tradeService;
    public TradesController(ITradeService tradeService)
    {
        _tradeService = tradeService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<int>> GetByIdAsync(int id)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<Trade>> BookAsync(CreateTradeRequestDto request,CancellationToken cancellationToken)
    {
        var trade = await _tradeService.BookAsync(request,null,cancellationToken);
        return CreatedAtAction( nameof(GetByIdAsync),new{id=trade.Id}, trade);
    }


}