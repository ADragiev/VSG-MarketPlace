using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.OrderModels.Dtos
{
    public class OrderGetMineDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }

        public int Qty { get; set; }

        public decimal Price { get; set; }

        public DateTime OrderDate { get; set; }

        public OrderStatus OrderStatus { get; set; }
    }
}
