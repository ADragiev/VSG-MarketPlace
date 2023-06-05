using Application.Models.GenericModels.Dtos;
using Application.Models.LendedItemModels.Dtos;
using Application.Models.OrderModels.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.LendedItemModels.Interfaces
{
    public interface ILendedItemService
    {
        Task CreateAsync(LendedItemCreateDto dto);

        Task ReturnItemAsync(int id);

        Task<Dictionary<string, List<LendedItemGetDto>>> GetAllLendedItemsGroupedByLenderAsync();
    }
}
