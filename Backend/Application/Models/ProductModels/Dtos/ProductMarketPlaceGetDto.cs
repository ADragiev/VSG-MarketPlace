using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ProductModels.Dtos
{
    public class ProductMarketPlaceGetDto
    {
        public int Id { get; set; }

        public string Category { get; set; }

        public string Location { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int SaleQty { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }
    }
}
