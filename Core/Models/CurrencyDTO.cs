namespace Core.Models
{
    public class CurrencyDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; } = null;
        public string? BuyValue { get; set; }
        public string? SellValue { get; set; }

    }
}
