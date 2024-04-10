using Core.Constants;

namespace Core.Entities
{
    public class CreditCard
    {
        public int Id { get; set; }
        public string? Designation {  get; set; } = string.Empty;
        public DateTime IssueDate  { get; set; }
        public DateTime ExpirationDT { get; set; }
        public string CardNumber { get; set; } = string.Empty ;
        public int Cvv {  get; set; }
        public CreditCardStatus CreditCardStatus { get; set; } = CreditCardStatus.Enabled;
        public Decimal CreditLimit { get; set; }
        public decimal AvailableCredit { get; set; }
        public decimal CurrentDebt { get; set; }
        public decimal InterestRate { get; set;}
        public int CustomerId { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; } = null!;
        public Customer Customer { get; set; } = null!;
        public ICollection<Customer>? customers { get; set; }
        public ICollection<Currency>? currency { get; set; }
    }
}
