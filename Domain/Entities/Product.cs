using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        public int Code { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }

        public ProductCategory Category { get; set; }

        public int SaleQty { get; set; }
        public int CombinedQty { get; set; }

        public string Description { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
