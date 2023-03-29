using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ProductModels.Dtos
{
    public class ProductGetDto
    {
        public string Picture { get; set; }

        public decimal Price { get; set; }

        public ProductCategory Category { get; set; }

        public int OrderQty { get; set; }
    }
}
