using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dagnys_api.ViewModels
{
    public class ProductOrderViewModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}