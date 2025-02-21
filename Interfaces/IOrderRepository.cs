using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dagnys_api.Entities;

namespace dagnys_api.Interfaces
{
    public interface IOrderRepository
    {
    Task<IList<Order>> List(DateTime? orderDate = null);
    Task<Order> Find(int id);
    Task<bool> Add(Order order);
    }

        
    
}

        
    
