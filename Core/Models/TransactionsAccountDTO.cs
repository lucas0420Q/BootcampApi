namespace Core.Models;

public class TransactionsAccountDTO
{
    public MovementDTO Movement { get; set; }
    public PaymentServiceDTO Payment { get; set; }
    public DepositDTO Deposit { get; set; }
    public ExtractionDTO Extraction { get; set; }

}
