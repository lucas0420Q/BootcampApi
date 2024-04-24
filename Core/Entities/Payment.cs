namespace Core.Entities;

public class Payment
{
    public int Id { get; set; }
    public string DocumentNumber { get; set; }
    public decimal Amount { get; set; }
    public int ServiceId { get; set; }
    public int AccountId { get; set; }
    public Account? Account { get; set; }
    public DateTime PaymentServiceDateTime { get; set; }
}