using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dagnys_api.Interfaces;
using dagnys_api.Repositories;

namespace dagnys_api.Data
{
    public class UnitOfWork : IUnitOfWork
    {
    private readonly DataContext _context;
    private readonly IAddressRepository _addressRepository;

    public UnitOfWork(DataContext context, IAddressRepository addressRepository)
    {
        _context = context;
        _addressRepository = addressRepository;
    }

    public ICustomerRepository CustomerRepository => new CustomerRepository(_context, _addressRepository);
    public IProductRepository ProductRepository => new ProductRepository(_context);
    public IOrderRepository OrderRepository => new OrderRepository(_context);
    public IAddressRepository AddressRepository => new AddressRepository(_context);

    public async Task<bool> Complete()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public bool HasChanges()
    {
        return _context.ChangeTracker.HasChanges();
    }
    }
}