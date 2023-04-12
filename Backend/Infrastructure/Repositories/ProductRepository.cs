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

        public async Task<List<ProductGetBaseDto>> GetAllIndexProducts()
        {
            var sql = @"SELECT p.Id, c.CategoryName AS Category, p.Price, p.SaleQty, i.ImageUrl AS Image
                        FROM 
                        Product AS p 
                        JOIN Category AS c ON p.CategoryId = c.Id 
                        LEFT JOIN Image AS i ON i.ProductId = p.Id
                        WHERE p.SaleQty > 0";

            var products = await Connection.QueryAsync<ProductGetBaseDto>(sql, null, Transaction);
            return products.ToList();
        }

        public async Task<List<ProductInventoryGetDto>> GetAllInventoryProducts()
        {
            var sql = @"SELECT p.Id, p.Code, p.FullName, c.CategoryName AS Category, p.SaleQty, p.CombinedQty
                        FROM
                        Product AS p
                        JOIN Category AS c ON p.CategoryId = c.Id";

            var products = await Connection.QueryAsync<ProductInventoryGetDto>(sql, null, Transaction);

            return products.ToList();
        }

        public async Task<ProductGetForUpdateDto> GetForEdit(int id)
        {
            var sql = @"SELECT p.Id, p.Code, p.FullName, p.Description, p.Price, p.SaleQty, p.Price, p.SaleQty, p.CombinedQty, c.Id AS CategoryId, i.ImageUrl AS Image
                        FROM Product AS p
                        JOIN Category AS c ON p.CategoryId = c.Id
						JOIN [Image] AS i On i.ProductId = p.Id
                        WHERE p.Id = @id";

            var productForEdit = await Connection.QueryFirstOrDefaultAsync<ProductGetForUpdateDto>(sql, new { id }, Transaction);

            return productForEdit;
        }

        public async Task<ProductDetailDto> GetProductDetail(int id)
        {
            var sql = @"SELECT p.Id, p.FullName, p.Price, c.CategoryName AS Category, p.SaleQty, p.Description, i.ImageUrl AS Image
                        FROM
                        Product AS p
                        JOIN Category AS c ON p.CategoryId  = c.Id
                        LEFT JOIN Image AS i ON i.ProductId = p.Id
                        WHERE p.Id = @id";

            var productDetail = await Connection.QueryFirstOrDefaultAsync<ProductDetailDto>(sql, new { id }, Transaction);

            return productDetail;
        }
    }
}
