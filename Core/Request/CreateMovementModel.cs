using Core.Constants;
using Core.Entities;
using Core.Models;

namespace Core.Request;

public class CreateMovementModel
{
    public string Description { get; set; } = string.Empty;
    public DateTime TransferredDateTime { get; set; }
    public MovementType MovementType { get; set; } = MovementType.Transfer;
    public decimal Amount { get; set; }
    public TransferStatus TransferStatus { get; set; } = TransferStatus.Pending;
    public int OriginalAccountId { get; set; }
    public int DestinationAccountId { get; set; }
    //public Account Account { get; set; } = null!;
    public string? DestinationAccountNumber { get; set; }
    public string? DestinationDocumentNumber { get; set; }
    public int CurrencyId { get; set; }
    public int DestinationBankId { get; set; }
}
