using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int SaleQty { get; set; }

        public int CombinedQty { get; set; }

        public string? Description { get; set; }

        public int CategoryId { get; set; }
        public int LocationId { get; set; }
    }
}
