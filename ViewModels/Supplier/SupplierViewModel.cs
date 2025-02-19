using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dagnys_api.ViewModels.Address;

namespace dagnys_api.ViewModels.Supplier
{
    public class SupplierViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<AddressViewModel> Addresses { get; set; }
    }
}