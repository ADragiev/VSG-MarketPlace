using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.LentItemModels.Dtos
{
    public class LentItemGetDto
    {
        public int Id { get; set; }

        public int Qty { get; set; }

        public string LentBy { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; } = null;

        public string ProductName { get; set; }

        public string ProductCode { get; set; }
    }
}
