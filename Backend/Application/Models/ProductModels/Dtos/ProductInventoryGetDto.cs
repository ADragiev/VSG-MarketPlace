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

        public string Name { get; set; }

        public string Category { get; set; }

        public int CategoryId { get; set; }

        public int SaleQty { get; set; }
        public int LendQty { get; set; }

        public int CombinedQty { get; set; }

        public string Image { get; set; }

        public string Location { get; set; }
        public int LocationId { get; set; }
    }
}
