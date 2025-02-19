using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dagnys_api.ViewModels.Customer;

namespace dagnys_api.Interfaces
{
    public interface ICustomerRepository
    {
    Task<IList<CustomerViewModel>> List();
    Task<CustomerViewModel> Find(int id);
    Task<bool> Add(CustomerPostViewModel model);
    Task<bool> Update(int id, CustomerPostViewModel model);
    Task<bool> Remove(int id);
    }
}