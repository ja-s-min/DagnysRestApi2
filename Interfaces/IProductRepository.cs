using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dagnys_api.Entities;

namespace dagnys_api.Interfaces
{
    public interface IProductRepository
    {
    Task<IList<Product>> List();
    Task<Product> Find(int id);
    Task<bool> Add(Product product);
    Task<bool> Update(int id, Product product);
    Task<bool> Remove(int id);
    }
}