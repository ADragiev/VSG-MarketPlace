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
        public int Id { get; set; }
        public int Qty { get; set; }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public string OrderedBy { get; set; }

        public string Date { get; set; }

        public string Status { get; set; }

        public int ProductId { get; set; }
    }
}
