using Core.Constants;
using Core.Models;

namespace Core.Entities;

public class Request
{
    public int Id { get; set; }
    public string Description { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime? ApprovalDate { get; set; } = null;
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public SolicitudRequestStatus Status { get; set; } = SolicitudRequestStatus.Pending;
    public int CurrencyId { get; set; }
    public Currency? Currency { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }


}