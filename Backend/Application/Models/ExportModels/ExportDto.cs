using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ExportModels
{
    public class ExportDto
    {
        [DisplayName("Category Name")]
        public string Name { get; set; }
    }
}
