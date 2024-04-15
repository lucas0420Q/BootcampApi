namespace Core.Request;
public class CreatePromotionModel
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Duracion { get; set; }
    public int PorcentajeDescuento { get; set; }
    public List<int>? BusinessId { get; set; } // Lista de IDs de empresas asociadas
}
