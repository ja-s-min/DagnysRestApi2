using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dagnys_api.ViewModels.Address;

namespace dagnys_api.ViewModels.Customer
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public List<AddressViewModel> Addresses { get; set; }
    }
}