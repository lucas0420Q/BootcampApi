namespace Core.Entities;

public class Extraction
{
    public int Id { get; set; }
    public int BankId { get; set; }
    public Bank Bank { get; set; }
    public decimal amount { get; set; }
    public DateTime DateExtraction { get; set; }
}
