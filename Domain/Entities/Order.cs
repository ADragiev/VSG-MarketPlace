using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        public int Code { get; set; }

        public int Qty { get; set; }

        public decimal Price { get; set; }

        public string OrderedBy { get; set; }

        public DateOnly OrderDate { get; set; }

        public OrderStatus Status { get; set; }

        public Product Product { get; set; }

        public User User { get; set; }
    }
}
