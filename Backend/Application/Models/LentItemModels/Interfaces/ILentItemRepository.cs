using Application.Models.GenericRepo;
using Application.Models.LentItemModels.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.LentItemModels.Interfaces
{
    public interface ILentItemRepository : IGenericRepository<LentItem>
    {
        Task<List<LentItemGetDto>> GetAllLentItemsAsync();

        Task<List<LentItemGetDto>> GetMyLentItemsAsync(string user);

        Task<List<LentItem>> GetProductLentItemsInUse(int productId);
    }
}
