using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Orders")]
    public class Order : BaseEntity
    {

        public int Qty { get; set; }

        public decimal Price { get; set; }

        public string OrderedBy { get; set; }

        public DateOnly OrderDate { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public int ProductCode { get; set; }
    }
}
