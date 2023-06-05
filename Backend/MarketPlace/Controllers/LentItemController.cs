using Application.Helpers.Attributes;
using Application.Models.GenericModels.Dtos;
using Application.Models.LentItemModels.Dtos;
using Application.Models.LentItemModels.Interfaces;
using Application.Models.OrderModels.Dtos;
using Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    public class LentItemController : BaseController
    {
        private readonly ILentItemService lendedItemService;
        private readonly IValidator<LentItemCreateDto> createValidator;

        public LentItemController(ILentItemService lendedItemService,
            IValidator<LentItemCreateDto> createValidator)
        {
            this.lendedItemService = lendedItemService;
            this.createValidator = createValidator;
        }

        [HttpGet]
        public async Task<Dictionary<string, List<LentItemForGroupGetDto>>> AllLendedItems()
        {
            return await lendedItemService.GetAllLendedItemsGroupedByLenderAsync();
        }

        [HttpPost]
        public async Task CreateOrder(LentItemCreateDto dto)
        {
            await createValidator.ValidateAndThrowAsync(dto);
            await lendedItemService.CreateAsync(dto);
        }


        [HttpPut("{id}")]
        public async Task RejectOrder(int id)
        {
            await lendedItemService.ReturnItemAsync(id);
        }
    }
}
