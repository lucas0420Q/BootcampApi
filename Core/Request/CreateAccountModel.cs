using Core.Constants;
using Core.Entities;
using Core.Models;

namespace Core.Request
{
    public class CreateAccountModel
    {
        public string Holder { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public AccountType Type { get; set; } = AccountType.Current;
        public int CurrencyId { get; set; }
        public int CustomerId { get; set; }
        public CreateSavingAccountDTO SavingAccount { get; set; }
        public CurrentAccountDTO CurrentAccount { get; set; }
        public SavingType SavingType { get; set; }
        public object OperationalLimit { get; set; }
        public object MonthAverage { get; set; }
    }
}




