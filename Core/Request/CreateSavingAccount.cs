using Core.Constants;

namespace Core.Request;

public class CreateSavingAccount
{
    public string HolderName { get; set; } = string.Empty;
    public SavingType SavingType { get; set; }
}