using Domain.Enums;

namespace Domain.Entities
{
    public class Order
    {
        public int OrderCode { get; set; }

        public int Qty { get; set; }

        public decimal Price { get; set; }

        public string OrderedBy { get; set; }

        public DateOnly OrderDate { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public int ProductCode { get; set; }
    }
}
