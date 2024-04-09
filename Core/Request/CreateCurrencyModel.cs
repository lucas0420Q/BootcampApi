namespace Core.Request
{
    public class CreateCurrencyModel
    {
        public string? Name { get; set; } = null;
        public string? BuyValue { get; set; } = string.Empty;
        public string? SellValue { get; set; } = string.Empty;
        public object?[]? CurrencyId { get; set; }
    }
}
