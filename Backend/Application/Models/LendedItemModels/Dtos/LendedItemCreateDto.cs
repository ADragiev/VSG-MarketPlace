using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.LendedItemModels.Dtos
{
    public class LendedItemCreateDto
    {
        public int Qty { get; set; }

        public int ProductId { get; set; }
        public string LendedBy { get; set; }
    }
}
