namespace Core.Entities;

public class ProductRequest
{
    public int ProductId { get; set; }
    public int RequestId { get; set; }

    public Product Product { get; set; } = null!;
    public Request Request { get; set; } = null!;

}
