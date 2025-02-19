using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dagnys_api.Interfaces;

namespace dagnys_api.Interfaces
{
    public interface IUnitOfWork
    {
    ICustomerRepository CustomerRepository { get; }
    IProductRepository ProductRepository { get; }
    IOrderRepository OrderRepository { get; }
    IAddressRepository AddressRepository { get; }
    
    Task<bool> Complete();
    bool HasChanges();
}

    
}