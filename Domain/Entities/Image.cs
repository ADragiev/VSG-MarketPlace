using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Images")]
    public class Image : BaseEntity
    {

        public string ImageUrl { get; set; }

        public int ProductCode { get; set; }

        public bool IsDefault { get; set; }
    }
}
