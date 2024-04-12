using Core.Constants;
using Core.Entities;

namespace Core.Models
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public string Holder { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public AccountType Type { get; set; } = AccountType.Current;
        public decimal Balance { get; set; }
        public AccountStatus Status { get; set; } = AccountStatus.Active;

        public int CurrencyId { get; set; }
        public Currency Currency { get; set; } = null!;

        public int CustomerId { get; set; }
        public Customers Customer { get; set; } = null!;

        public bool IsDelete { get; set; } = false;
        public CreateSavingAccountDTO SavingAccount { get; set; }
        public CurrentAccountDTO CurrentAccount { get; set; } 
    }
}
