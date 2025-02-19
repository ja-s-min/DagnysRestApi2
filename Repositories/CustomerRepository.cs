using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dagnys_api.Data;
using dagnys_api.Interfaces;
using Microsoft.EntityFrameworkCore;
using dagnys_api.Entities;
using dagnys_api.ViewModels.Customer;
using dagnys_api.ViewModels.Address;

namespace dagnys_api.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly DataContext _context;
    private readonly IAddressRepository _addressRepository;

    public CustomerRepository(DataContext context, IAddressRepository addressRepository)
    {
        _context = context;
        _addressRepository = addressRepository;
    }

    public async Task<IList<CustomerViewModel>> List()
    {
        var customers = await _context.Customers.ToListAsync();
        return customers.Select(c => new CustomerViewModel { Id = c.Id, StoreName = c.StoreName }).ToList();
    }

    public async Task<CustomerViewModel> Find(int id)
    {
        var customer = await _context.Customers
            .Include(c => c.CustomerAddresses)
            .ThenInclude(ca => ca.Address)
            .SingleOrDefaultAsync(c => c.Id == id);

        if (customer == null) return null;

        return new CustomerViewModel
        {
            Id = customer.Id,
            StoreName = customer.StoreName,
            Email = customer.Email,
            Phone = customer.Phone,
            ContactPerson = customer.ContactPerson,
            Addresses = customer.CustomerAddresses.Select(ca => new AddressViewModel
            {
                AddressLine = ca.Address.AddressLine,
                City = ca.Address.PostalAddress.City,
                PostalCode = ca.Address.PostalAddress.PostalCode
            }).ToList()
        };
    }

    public async Task<bool> Add(CustomerPostViewModel model)
    {
        var customer = new Customer
        {
            StoreName = model.StoreName,
            Phone = model.Phone,
            Email = model.Email,
            ContactPerson = model.ContactPerson
        };

        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
        return true;
    }

    public Task<bool> Update(int id, CustomerPostViewModel model)
    {
        
        throw new NotImplementedException();
    }

    public Task<bool> Remove(int id)
    {
        
        throw new NotImplementedException();
    }
}
