using Core.Constants;
using Core.Models;

namespace Core.Entities;

public class Movement
{
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public MovementType MovementType { get; set; } = MovementType.Transfer;
        public decimal Amount { get; set; }
        public DateTime? TransferredDateTime { get; set; }
        public TransferStatus TransferStatus { get; set; } = TransferStatus.Pending;
        public int OriginalAccountId { get; set; }
        public int DestinationAccountId { get; set; }
        public Account Account { get; set; } = null!;

}
