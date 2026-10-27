public class Bond
{
    public int Id {get;set;}
    public string Isin {get;set;} = string.Empty;
    public string Issuer {get;set;} = string.Empty;
    public string Currency {get;set;} = string.Empty;
    public decimal CouponRate { get;set;}
    public DateOnly MaturityDate {get;set;}

}