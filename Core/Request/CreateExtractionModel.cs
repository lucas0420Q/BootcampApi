namespace Core.Request;

public class CreateExtractionModel
{
    public int AccountId { get; set; }
    public int BankId { get; set; }
    public decimal amount { get; set; }
    public DateTime DateExtraction { get; set; }
}
