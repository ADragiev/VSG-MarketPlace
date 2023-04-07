using Application.Models.CategoryModels.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ProductModels.Dtos
{
    public class ProductUpdatetDto
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string FullName { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public decimal Price { get; set; }

        public int SaleQty { get; set; }

        public int CombinedQty { get; set; }
    }
}
