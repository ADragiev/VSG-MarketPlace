using Application.Models.GenericRepo;
using Application.Models.ImageModels.Dtos;
using Application.Models.ProductModels.Dtos;
using Application.Models.ProductModels.Intefaces;
using Dapper;
using Domain.Entities;
using Infrastructure.Repositories.GenericRepository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(IMarketPlaceContext marketPlaceContext)
            : base(marketPlaceContext)
        {
        }

        public async Task<List<ProductMarketPlaceGetDto>> GetAllIndexProductsAsync()
        {
            var sql = @"SELECT * FROM IndexProducts";

            var products = await connection.QueryAsync<ProductMarketPlaceGetDto>(sql, null, transaction);
            return products.ToList();
        }

        public async Task<List<ProductInventoryGetDto>> GetAllInventoryProductsAsync()
        {
            var sql = @"SELECT * FROM InventoryProducts";

            var products = await connection.QueryAsync<ProductInventoryGetDto>(sql, null, transaction);

            return products.ToList();
        }

        public async Task<int> GetProductPendingOrdersCountAsync(int productId)
        {
            var sql = "SELECT dbo.GetProductPendingOrdersCount(@productId)";

            var count = await connection.QueryFirstOrDefaultAsync<int>(sql, new { productId }, transaction);

            return count;
        }
    }
}
