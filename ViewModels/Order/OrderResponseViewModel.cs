using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dagnys_api.ViewModels;

namespace dagnys_api.ViewModels.Order
{
    public class OrderResponseViewModel
    {
         public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public CustomerViewModel Customer { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
        public double TotalPrice { get; set; }
    }
}