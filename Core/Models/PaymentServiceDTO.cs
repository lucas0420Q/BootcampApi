using Core.Entities;

namespace Core.Models;

public class PaymentServiceDTO
{
    public int Id { get; set; }
    public string DocumentNumber { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public int AccountId { get; set; }
    public Account Account { get; set; }
    public DateTime PaymentServiceDateTime { get; set; }
}
