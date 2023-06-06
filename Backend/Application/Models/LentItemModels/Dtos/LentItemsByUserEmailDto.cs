using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.LentItemModels.Dtos
{
    public class LentItemsByUserEmailDto
    {
        public string Email { get; set; }

        public List<LentItemWithoutUserGetDto> LentItems { get; set; }
    }
}
