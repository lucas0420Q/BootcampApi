public class FilterPromotionModel
{
    public string? Id { get; set; } 
    public string Name { get; set; } = string.Empty;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int Discount { get; set; }
}