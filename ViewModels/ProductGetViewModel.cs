using dagnys_api.ViewModels;

namespace dagnys_api.ViewModels;

public class ProductGetViewModel
{
  public int Id { get; set; }
  public string ProductName { get; set; }
  public decimal Price { get; set; }
  public double Weight { get; set; }
  public int PackSize { get; set; }
}



