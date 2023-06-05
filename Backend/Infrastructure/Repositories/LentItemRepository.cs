using Application.Models.GenericRepo;
using Application.Models.LentItemModels.Dtos;
using Application.Models.LentItemModels.Interfaces;
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
    public class LentItemRepository : GenericRepository<LentItem>, ILentItemRepository
    {
        public LentItemRepository(IMarketPlaceContext marketPlaceContext)
            : base(marketPlaceContext)
        {
        }

        public async Task<List<LentItemGetDto>> GetAllLentItemsAsync()
        {
            var sql = @"SELECT li.Id, li.Qty, li.LentBy, li.StartDate, li.EndDate, p.Name AS ProductName, p.Code AS ProductCode
                        FROM
                        LentItem AS li
                        JOIN Product AS p ON li.ProductId = p.Id";

            var lendedItems = await connection.QueryAsync<LentItemGetDto>(sql, null, transaction);
            return lendedItems.ToList();
        }

        public async Task<List<LentItemGetDto>> GetMyLentItemsAsync(string user)
        {
            var sql = @"SELECT li.Id, li.Qty, li.LentBy, li.StartDate, li.EndDate, p.Name AS ProductName, p.Code AS ProductCode
                        FROM
                        LentItem AS li
                        JOIN Product AS p ON li.ProductId = p.Id
                        WHERE LentBy = @user";

            var myLentItems = await connection.QueryAsync<LentItemGetDto>(sql, new { user }, transaction);
            return myLentItems.ToList();
        }

        public async Task<List<LentItem>> GetProductLentItemsInUse(int productId)
        {
            var sql = @"SELECT * FROM
                        LentItem
                        WHERE ProductId = @productId AND EndDate IS NULL";

            var inUseLendedItems = await connection.QueryAsync<LentItem>(sql, new { productId }, transaction);
            return inUseLendedItems.ToList();
        }
    }
}
