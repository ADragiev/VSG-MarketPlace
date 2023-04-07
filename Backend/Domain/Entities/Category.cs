using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Categories")]
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
    }
}
