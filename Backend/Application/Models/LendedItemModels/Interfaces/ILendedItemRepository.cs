using Application.Models.GenericRepo;
using Application.Models.LendedItemModels.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.LendedItemModels.Interfaces
{
    public interface ILendedItemRepository : IGenericRepository<LendedItem>
    {
        Task<List<LendedItemGetDto>> GetAllLendedItemsAsync();
    }
}
