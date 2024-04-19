using Core.Constants;

namespace Core.Request;
public class CreateRequestModel
{
    public string Description { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime? ApprovalDate { get; set; } = null;
    public SolicitudRequestStatus Status { get; set; }
    public int CustomerId { get; set; }
    public int CurrencyId { get; set; }
}

