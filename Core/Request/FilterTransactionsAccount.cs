using Core.Models;

namespace Core.Request;

public class FilterTransactionsAccount
{
    public int? AccountId { get; set; }
    public DateTime? Month { get; set; }
    public DateTime? Year { get; set; }
    public int? DateYearFrom { get; set; }
    public int? DateYearTo { get; set; }
    public string? Descripción { get; set; }

}
