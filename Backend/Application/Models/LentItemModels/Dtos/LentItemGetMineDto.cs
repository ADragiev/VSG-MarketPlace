using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.LentItemModels.Dtos
{
    public class LentItemGetMineDto
    {
        public int Qty { get; set; }

        public string StartDate { get; set; }

        public string? EndDate { get; set; } = null;

        public string ProductName { get; set; }

        public string ProductCode { get; set; }
    }
}
