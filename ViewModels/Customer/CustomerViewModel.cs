using dagnys_api.Entities;
using dagnys_api.ViewModels.Address;
using dagnys_api.ViewModels;

namespace dagnys_api;

public class CustomerViewModel : CustomerBaseViewModel
{
  public int Id { get; set; }
  public IList<AddressViewModel> Addresses { get; set; }
   public IList<OrderViewModel> Orders { get; set; }
}
