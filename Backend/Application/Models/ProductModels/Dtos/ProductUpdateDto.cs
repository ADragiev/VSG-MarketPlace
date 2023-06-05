using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ProductModels.Dtos
{
    public class ProductUpdateDto
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public int CategoryId { get; set; }
        public int LocationId { get; set; }

        public decimal? Price { get; set; }

        public int? SaleQty { get; set; }
        public int? LendQty { get; set; }

        public int CombinedQty { get; set; }
    }
}
