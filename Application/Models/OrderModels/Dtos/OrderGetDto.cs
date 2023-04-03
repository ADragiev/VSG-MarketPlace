using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.OrderModels.Dtos
{
    public class OrderGetDto
    {
        public int Code { get; set; }
        public int Qty { get; set; }

        public string OrderedBy { get; set; }

        public DateTime OrderDate { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public int ProductCode { get; set; }
    }
}
