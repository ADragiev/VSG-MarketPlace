namespace Domain.Entities
{
    public class Product
    {
        public int ProductCode { get; set; }

        public string FullName { get; set; }

        public decimal Price { get; set; }

        public int SaleQty { get; set; }

        public int CombinedQty { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

    }
}
