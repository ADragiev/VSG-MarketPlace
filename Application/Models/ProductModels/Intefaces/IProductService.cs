using Application.Models.ProductModels.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ProductModels.Intefaces
{
    public interface IProductService
    {
        List<ProductGetDto> GetAll();
    }
}
