using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Products")]
    public class Product : BaseEntity
    {

        public string FullName { get; set; }

        public decimal Price { get; set; }

        public int SaleQty { get; set; }

        public int CombinedQty { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

    }
}
