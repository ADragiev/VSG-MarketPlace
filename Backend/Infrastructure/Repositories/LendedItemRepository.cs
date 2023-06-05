using Application.Models.GenericRepo;
using Application.Models.LendedItemModels.Dtos;
using Application.Models.LendedItemModels.Interfaces;
using Dapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Infrastructure.Repositories
{
    public class LendedItemRepository : GenericRepository<LendedItem>, ILendedItemRepository
    {
        public LendedItemRepository(IMarketPlaceContext marketPlaceContext)
            : base(marketPlaceContext)
        {
        }

        public async Task<List<LendedItemGetDto>> GetAllLendedItemsAsync()
        {
            var sql = @"SELECT li.Id, li.Qty, li.LendedBy, li.StartDate, li.EndDate, p.Name AS ProductName, p.Code AS ProductCode
                        FROM
                        LendedItem AS li
                        JOIN Product AS p ON li.ProductId = p.Id";

            var lendedItems = await connection.QueryAsync<LendedItemGetDto>(sql, null, transaction);
            return lendedItems.ToList();
        }

        public async Task<List<LendedItem>> GetProductLendedItemsInUse(int productId)
        {
            var sql = @"SELECT * FROM
                        LendedItem
                        WHERE ProductId = @productId AND EndDate IS NULL";

            var inUseLendedItems = await connection.QueryAsync<LendedItem>(sql, new { productId }, transaction);
            return inUseLendedItems.ToList();
        }
    }
}
