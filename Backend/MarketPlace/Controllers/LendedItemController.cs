using Application.Helpers.Attributes;
using Application.Models.GenericModels.Dtos;
using Application.Models.LendedItemModels.Dtos;
using Application.Models.LendedItemModels.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    public class LendedItemController : BaseController
    {
        private readonly ILendedItemService lendedItemService;

        public LendedItemController(ILendedItemService lendedItemService)
        {
            this.lendedItemService = lendedItemService;
        }

        [HttpGet]
        public async Task<Dictionary<string, List<LendedItemGetDto>>> AllLendedItems()
        {
            return await lendedItemService.GetAllLendedItemsGroupedByLenderAsync();
        }

        [HttpPost]
        public async Task CreateOrder(LendedItemCreateDto dto)
        {
            await lendedItemService.CreateAsync(dto);
        }


        [HttpPut("{id}")]
        public async Task<GenericSimpleValueGetDto<string>> RejectOrder(int id)
        {
            return await lendedItemService.ReturnItemAsync(id);
        }
    }
}
