namespace Core.Request;

public class CreateDepositModel
{
    public int AccountId { get; set; }
    public int BankId { get; set; }
    public decimal amount { get; set; }
    public DateTime DateOperation { get; set; }
}
