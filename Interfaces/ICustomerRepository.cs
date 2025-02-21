using System.Threading.Tasks;
using dagnys_api.Entities;
using dagnys_api.ViewModels.Address;
using Microsoft.EntityFrameworkCore;
using dagnys_api.Interfaces;

namespace dagnys_api.Interfaces;
public interface ICustomerRepository
{
  public Task<IList<CustomersViewModel>> List();
  public Task<CustomerViewModel> Find(int id);
  public Task<bool> Add(CustomerPostViewModel model);

}
