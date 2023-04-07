using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Images")]
    public class Image : BaseEntity
    {

        public string ImageUrl { get; set; }

        public string ImagePublicId { get; set; }

        public int ProductId { get; set; }
    }
}
