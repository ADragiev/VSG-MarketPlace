using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Image : BaseEntity
    {
        public string PublicId { get; set; }

        public int ProductId { get; set; }
    }
}
