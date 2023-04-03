using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Orders")]
    public class Order : BaseEntity
    {
        public int Qty { get; set; }

        public string OrderedBy { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

        public int ProductCode { get; set; }
    }
}
