using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class LendedItem : BaseEntity
    {
        public int Qty { get; set; }

        public string LendedBy { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime? EndDate { get; set; } = null;

        public LendedItemStatus Status { get; set; } = LendedItemStatus.InUse;

        public int ProductId { get; set; }
    }
}
