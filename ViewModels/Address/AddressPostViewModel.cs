using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dagnys_api.ViewModels;

namespace dagnys_api.ViewModels.Address
{
    public class AddressPostViewModel
    {
        public string AddressLine { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        
       
        public int AddressTypeId { get; set; }
    }
}