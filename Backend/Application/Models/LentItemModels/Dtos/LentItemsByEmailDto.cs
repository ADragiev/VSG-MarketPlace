using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.LentItemModels.Dtos
{
    public class LentItemsByEmailDto
    {
        public string Email { get; set; }

        public List<LentItemForGroupGetDto> LentItems { get; set; }
    }
}
