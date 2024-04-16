
using Core.Constants;
using Core.Requests;

namespace Core.Request;

public class CreateSavingAccountRequest
{
    public string Holder { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    //public string Type { get; set; } = string.Empty; //ojo
    public int CustomerId { get; set; }
    public int CurrencyId { get; set; }
    public AccountType AccountType { get; set; }
    public CreateSavingAccount? CreateSavingAccount { get; set; }
    public CreateCurrentAccount? CreateCurrentAccount { get; set; }
}
