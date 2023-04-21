using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.OrderModels.Dtos
{
    public class OrderPendingDto
    {
        public int Id { get; set; }

        public string ProductCode { get; set; }

        public int Qty { get; set; }

        public decimal Price { get; set; }

        public string OrderedBy { get; set; }

        public string Date { get; set; }

    }
}
