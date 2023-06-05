using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.LendedItemModels.Dtos
{
    public class LendedItemGetDto
    {
        public int Id { get; set; }

        public int Qty { get; set; }

        public string LendedBy { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; } = null;

        public string ProductName { get; set; }

        public string ProductCode { get; set; }
    }
}
