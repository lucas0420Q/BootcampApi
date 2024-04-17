using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Request
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime RequestDate { get; set; }

    public DateTime? BankApprovalDate { get; set; }

    public int ProductId { get; set; } // Clave foránea

    public Product Product { get; set; } = null!; // Propiedad de navegación



    //public ICollection<Currency> Currencies { get; set; } = new List<Currency>();


}
