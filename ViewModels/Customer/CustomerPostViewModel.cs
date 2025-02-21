using dagnys_api.ViewModels.Address;

namespace dagnys_api;

public class CustomerPostViewModel : CustomerBaseViewModel
{
  public IList<AddressPostViewModel> Addresses { get; set; }
}
