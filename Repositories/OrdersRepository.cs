using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dagnys_api.Data;
using dagnys_api.Interfaces;
using dagnys_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace dagnys_api.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DataContext _context;

    public OrderRepository(DataContext context)
    {
        _context = context;
    }


    public async Task<bool> Add(Order order)
    {
        await _context.Orders.AddAsync(order);
        foreach (var orderItem in order.OrderItems)
        {
            await _context.OrderItems.AddAsync(orderItem);
        }
        return await _context.SaveChangesAsync() > 0;
    }

    
    public async Task<Order> Find(int id)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.OrderId == id);
    }

    
    public async Task<IList<Order>> List(DateTime? orderDate = null)
    {
        var query = _context.Orders.AsQueryable();

        if (orderDate.HasValue)
        {
            query = query.Where(o => o.OrderDate.Date == orderDate.Value.Date);
        }

        return await query
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .ToListAsync();
    }
}

    