using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dagnys_api.Data;
using dagnys_api.Interfaces;
using dagnys_api.Entities;
using dagnys_api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace dagnys_api.Repositories;
public class ProductRepository : IProductRepository
{
    private readonly DataContext _context;

    public ProductRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IList<Product>> List()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product> Find(int id)
    {
        return await _context.Products.SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<bool> Add(Product product)
    {
        await _context.Products.AddAsync(product);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(int id, Product product)
    {
        var existingProduct = await _context.Products.SingleOrDefaultAsync(p => p.Id == id);
        if (existingProduct == null) return false;

        existingProduct.Price = product.Price;
        existingProduct.ProductName = product.ProductName;
        existingProduct.Weight = product.Weight;
        existingProduct.PackSize = product.PackSize;
        existingProduct.BestBefore = product.BestBefore;
        existingProduct.ManufactureDate = product.ManufactureDate;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Remove(int id)
    {
        var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == id);
        if (product == null) return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}