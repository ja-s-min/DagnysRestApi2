using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dagnys_api.ViewModels
{
    public class OrderViewModel
    {
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public List<ProductOrderViewModel> Products { get; set; }
    }
}