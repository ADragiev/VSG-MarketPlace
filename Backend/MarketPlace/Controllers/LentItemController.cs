using Application.Helpers.Attributes;
using Application.Models.GenericModels.Dtos;
using Application.Models.LentItemModels.Dtos;
using Application.Models.LentItemModels.Interfaces;
using Application.Models.OrderModels.Dtos;
using Application.Models.OrderModels.Interfaces;
using Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    public class LentItemController : BaseController
    {
        private readonly ILentItemService lentItemService;
        private readonly IValidator<LentItemCreateDto> createValidator;

        public LentItemController(ILentItemService lentItemService,
            IValidator<LentItemCreateDto> createValidator)
        {
            this.lentItemService = lentItemService;
            this.createValidator = createValidator;
        }

        [HttpGet]
        public async Task<Dictionary<string, List<LentItemForGroupGetDto>>> AllLendedItems()
        {
            return await lentItemService.GetAllLentItemsGroupedByLenderAsync();
        }

        [HttpPost]
        public async Task CreateOrder(LentItemCreateDto dto)
        {
            await createValidator.ValidateAndThrowAsync(dto);
            await lentItemService.CreateAsync(dto);
        }


        [HttpPut("{id}")]
        public async Task RejectOrder(int id)
        {
            await lentItemService.ReturnItemAsync(id);
        }

        [HttpGet("My-Lent-Items")]
        [NonAdmin]
        public async Task<List<LentItemGetMineDto>> GetMyLentItems()
        {
            return await lentItemService.GetMyLentItemsAsync();
        }
    }
}
