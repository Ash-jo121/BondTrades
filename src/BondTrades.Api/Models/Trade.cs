public class Trade
{
    public int Id {get;set;}
    public int BondId {get;set;}
    public string Book {get;set;} = string.Empty;
    public TradeSide Side {get;set;}
    public decimal Quantity {get;set;}
    public decimal Price {get;set;}
    public DateTime TradeDate {get;set;}
    public TradeStatus Status {get;set;}
    public int RowVersion {get;set;}
    public string? IdempotencyKey {get;set;} = string.Empty;
    public DateTime CreatedAt {get;set;}

}