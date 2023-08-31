using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models.ExportModels
{
    public class ProductExportDto
    {
        [DisplayName("Code")]
        public string Code { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Combined quantity")]
        public int CombinedQty { get; set; }

        [NotMapped]
        public int LentQty { get; set; }

        [NotMapped]
        public int LendQty { get; set; }

        [DisplayName("Lent/Total")]
        public string LentLend => $"{LentQty}/{LendQty}";

        [DisplayName("Sale quantity")]
        public int SaleQty { get; set; }

        [DisplayName("Location")]
        public string Location { get; set; }

        [DisplayName("Category")]
        public string Category { get; set; }
    }
}
