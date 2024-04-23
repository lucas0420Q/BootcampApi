using Core.Constants;

namespace Core.Request;

public class CreateRequestModel
{

    public string Description { get; set; }
    public DateTime RequestDate { get; set; }
    //public int ProductId { get; set; }
    public SolicitudRequestStatus Status { get; set; } = SolicitudRequestStatus.Pending;
    public int CustomerId { get; set; }
    public int CurrencyId { get; set; }
}
