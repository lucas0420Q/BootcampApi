using Core.Entities;

namespace Core.Models;

public class PaymentServiceDTO
{
    public int Id { get; set; }
    public string DocumentNumber { get; set; }
    public decimal Amount { get; set; }
    public int ServiceId { get; set; }
    public int AccountId { get; set; }
    public AccountDTO Account { get; set; }
    public DateTime PaymentServiceDateTime { get; set; }
}
