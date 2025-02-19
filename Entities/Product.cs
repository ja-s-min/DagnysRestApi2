
namespace dagnys_api.Entities;
public class Product
{
    public int Id { get; set; } 
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public double Weight { get; set; }
    public int PackSize { get; set; }
    public DateTime BestBefore { get; set; }
    public DateTime ManufactureDate { get; set; }

    public IList<OrderItem> OrderItems { get; set; }
}
