using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.LentItemModels.Dtos
{
    public class LentItemCreateDto
    {
        public int Qty { get; set; }

        public int ProductId { get; set; }
        public string LentBy { get; set; }
    }
}
