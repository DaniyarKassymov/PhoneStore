namespace Lesson.Options;

public class CurrencyInfo
{
    public const string Name = "CurrencyInfo";
    
    public required IEnumerable<Currency> Currencies { get; set; }
}

public class Currency
{
    public required string CurrencyCode { get; set; }
    public required string CurrencyName { get; set; }
    public required short CurrencyRate { get; set; }
}