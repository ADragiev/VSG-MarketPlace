using Application.Models.ExceptionModels;
using Application.Models.GenericModels.Dtos;
using Application.Models.LentItemModels.Dtos;
using Application.Models.LentItemModels.Interfaces;
using Application.Models.ProductModels.Intefaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class LentItemModelsService : ILentItemService
    {
        private readonly ILentItemRepository lendedItemRepo;
        private readonly IProductRepository productRepo;
        private readonly IMapper mapper;

        public LentItemModelsService(ILentItemRepository lendedItemRepo,
            IProductRepository productRepo, 
            IMapper mapper)
        {
            this.lendedItemRepo = lendedItemRepo;
            this.productRepo = productRepo;
            this.mapper = mapper;
        }
        public async Task CreateAsync(LentItemCreateDto dto)
        {
            var product = await productRepo.GetByIdAsync(dto.ProductId);

            if (product == null || product.IsDeleted)
            {
                throw new HttpException($"Product Id not found!", HttpStatusCode.NotFound);
            }

            if (dto.Qty > product.LendQty)
            {
                throw new HttpException("Not enough quantity for lend!", HttpStatusCode.BadRequest);
            }

            var newLendQty = product.LendQty - dto.Qty;
            await productRepo.SetFieldAsync(product.Id, "LendQty", newLendQty);

            var newCombinedQty = product.CombinedQty - dto.Qty;
            await productRepo.SetFieldAsync(product.Id, "CombinedQty", newCombinedQty);

            var lendedItem = mapper.Map<LentItem>(dto);
            await lendedItemRepo.CreateAsync(lendedItem);
        }

        public async Task<Dictionary<string, List<LentItemForGroupGetDto>>> GetAllLendedItemsGroupedByLenderAsync()
        {
            var lendedItems = await lendedItemRepo.GetAllLendedItemsAsync();

            return lendedItems.GroupBy(l => l.LentBy)
                .ToDictionary(g => g.Key, g => mapper.Map<List<LentItemForGroupGetDto>>(g.ToList()));
        }

        public async Task ReturnItemAsync(int id)
        {
            var lendedItem = await lendedItemRepo.GetByIdAsync(id);


            if (lendedItem == null)
            {
                throw new HttpException($"Lended item Id not found!", HttpStatusCode.NotFound);
            }

            if (lendedItem.EndDate != null)
            {
                throw new HttpException("Only lended items without end date can be returnded!", HttpStatusCode.BadRequest);
            }

            var product = await productRepo.GetByIdAsync(lendedItem.ProductId);

            var newLendQty = product.LendQty + lendedItem.Qty;
            await productRepo.SetFieldAsync(product.Id, "LendQty", newLendQty);

            var newCombinedQty = product.CombinedQty + lendedItem.Qty;
            await productRepo.SetFieldAsync(product.Id, "CombinedQty", newCombinedQty);

            await lendedItemRepo.SetFieldAsync(id, "EndDate", DateTime.Now);

        }
    }
}
