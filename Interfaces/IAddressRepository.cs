using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dagnys_api.Entities;
using dagnys_api.Data;
using Microsoft.EntityFrameworkCore;
using dagnys_api.ViewModels.Address;

namespace dagnys_api;

    public interface IAddressRepository
    {
    Task<Address> Add(AddressPostViewModel model);
    Task<bool> Add(string type);
    Task<bool> Remove(int id);
    Task<Address> Find(int id);
    }

    