using System.ComponentModel.DataAnnotations;

public class CreateTradeRequestDto
{
    public string BondIsin {get;set;}
    [Required]
    public string Book {get;set;} = string.Empty;
    public TradeSide Side {get;set;}
    [Range(0.01, double.MaxValue)]
    public decimal Quantity {get;set;}
    public decimal Price {get;set;}
}