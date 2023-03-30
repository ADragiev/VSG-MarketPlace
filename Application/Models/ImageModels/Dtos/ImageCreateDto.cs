using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ImageModels.Dtos
{
    public class ImageCreateDto
    {
        public string ImageUrl { get; set; }
        public bool IsDefault { get; set; }
    }
}
