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

            var products = await Connection.QueryAsync<ProductMarketPlaceGetDto>(sql, null, Transaction);
            return products.ToList();
        }

        public async Task<List<ProductInventoryGetDto>> GetAllInventoryProductsAsync()
        {
            var sql = @"SELECT * FROM InventoryProducts";

            var products = await Connection.QueryAsync<ProductInventoryGetDto>(sql, null, Transaction);

            return products.ToList();
        }
    }
}
