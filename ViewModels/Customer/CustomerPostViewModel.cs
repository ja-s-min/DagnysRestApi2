using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dagnys_api.ViewModels.Address;
using Microsoft.EntityFrameworkCore;
using dagnys_api.Data;
using dagnys_api.Controllers;
using dagnys_api.Repositories;
using dagnys_api.Interfaces;
using Microsoft.AspNetCore;

namespace dagnys_api.ViewModels.Customer
{
    public class CustomerPostViewModel
    {
        public string StoreName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public List<AddressPostViewModel> Addresses { get; set; }
    }
}