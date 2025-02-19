using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dagnys_api.Entities
{
    public class Order
    {
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    }
}