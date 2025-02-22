using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dagnys_api.Data;
using dagnys_api.Entities;
using dagnys_api.Interfaces;
using dagnys_api.ViewModels.Address;
using System.Collections.Generic;
using dagnys_api.ViewModels;

namespace dagnys_api.Repositories;
public class CustomerRepository(DataContext context, IAddressRepository repo) : ICustomerRepository
{
  private readonly DataContext _context = context;
  private readonly IAddressRepository _repo = repo;

  public async Task<bool> Add(CustomerPostViewModel model)
  {
    try
    {
      if (await _context.Customers.FirstOrDefaultAsync(c => c.Email.ToLower().Trim()
        == model.Email.ToLower().Trim()) != null)
      {
        throw new Exception("Kunden finns redan");
      }

      var customer = new Customer
      {
        StoreName = model.StoreName,
        Email = model.Email,
        Phone = model.Phone,
        ContactPerson = model.ContactPerson
      };

      await _context.AddAsync(customer);

      foreach (var a in model.Addresses)
      {
        var address = await _repo.Add(a);

        await _context.CustomerAddresses.AddAsync(new CustomerAddress
        {
          Address = address,
          Customer = customer
        });
      }
      return await _context.SaveChangesAsync() > 0;
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }

public async Task<CustomerViewModel> Find(int id)
{
    try
    {
        var customer = await _context.Customers
        .AsNoTracking()
            .Where(c => c.Id == id)
            .Include(c => c.CustomerAddresses)
                .ThenInclude(c => c.Address)
                .ThenInclude(c => c.PostalAddress)
            .Include(c => c.CustomerAddresses)
                .ThenInclude(c => c.Address)
                .ThenInclude(c => c.AddressType)
            .Include(c => c.Orders) 
                .ThenInclude(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .SingleOrDefaultAsync();

        if (customer is null)
        {
            throw new Exception($"Det finns ingen kund med id {id}");
        }

        var view = new CustomerViewModel
        {
            Id = customer.Id,
            StoreName = customer.StoreName,
            Email = customer.Email,
            Phone = customer.Phone,
            ContactPerson = customer.ContactPerson,
            Addresses = customer.CustomerAddresses.Select(c => new AddressViewModel
            {
                AddressLine = c.Address.AddressLine,
                PostalCode = c.Address.PostalAddress.PostalCode,
                City = c.Address.PostalAddress.City,
                AddressType = c.Address.AddressType.Value
            }).ToList(),
            Orders = customer.Orders.Select(o => new OrderViewModel
            {
                OrderDate = o.OrderDate,
                CustomerId = customer.Id,
                Products = o.OrderItems.Select(oi => new ProductOrderViewModel
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            }).ToList()
        };
        
        return view;
    }
    catch (Exception ex)
    {
        throw new Exception(ex.Message);
    }
}

  public async Task<IList<CustomersViewModel>> List()
  {
    var response = await _context.Customers.ToListAsync();
    var customers = response.Select(c => new CustomersViewModel
    {
      Id = c.Id,
      StoreName = c.StoreName,
      Email = c.Email,
      Phone = c.Phone,
      ContactPerson = c.ContactPerson
    });

    return [.. customers];
  }

public void Attach(Customer customer)
{
    _context.Customers.Attach(customer);
    _context.Entry(customer).Property(c => c.ContactPerson).IsModified = true;
}
}







