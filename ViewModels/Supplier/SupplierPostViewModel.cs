using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dagnys_api.ViewModels.Address;


namespace dagnys_api.ViewModels.Supplier
{
    public class SupplierPostViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<AddressPostViewModel> Addresses { get; set; }
    }
}