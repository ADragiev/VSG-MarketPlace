using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ProductModels.Dtos
{
    public class ProductInventoryGetDto
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string FullName { get; set; }

        public string Category { get; set; }

        public int SaleQty { get; set; }

        public int CombinedQty { get; set; }

        public string Image { get; set; }
    }
}
