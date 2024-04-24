namespace Core.Models;

public class ExtractionDTO
{
    public int Id { get; set; }
    public int BankId { get; set; }
    public BankDTO Bank { get; set; }
    public decimal amount { get; set; }
    public DateTime DateExtraction { get; set; }
}
