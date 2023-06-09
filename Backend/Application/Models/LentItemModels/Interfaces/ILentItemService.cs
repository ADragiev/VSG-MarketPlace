﻿using Application.Models.GenericModels.Dtos;
using Application.Models.LentItemModels.Dtos;
using Application.Models.OrderModels.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.LentItemModels.Interfaces
{
    public interface ILentItemService
    {
        Task CreateAsync(LentItemCreateDto dto);

        Task<GenericSimpleValueGetDto<string>> ReturnItemAsync(int id);

        Task<List<LentItemsByUserEmailDto>> GetAllLentItemsGroupedByUserAsync();

        Task<List<LentItemGetMineDto>> GetMyLentItemsAsync();
    }
}
