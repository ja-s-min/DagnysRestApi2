using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dagnys_api.Entities
{
    public class OrderItem
    {
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }

    public Product Product { get; set; }
    }
}