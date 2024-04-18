using Core.Constants;
using Core.Entities;

namespace Core.Models;

public class RequestDTO
{
    public int Id { get; set; }
    public string Description { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime? ApprovalDate { get; set; }
    public string ProductType { get; set; }
    public string Status { get; set; } 
    public string Currency { get; set; } = null!;
    public string Customer { get; set; } = null!;

}
