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
            var sql = @"SELECT p.Id, c.CategoryName AS Category, p.Price, p.SaleQty, i.ImageUrl AS DefaultImage
                        FROM 
                        Products AS p 
                        JOIN Categories AS c ON p.CategoryId = c.Id 
                        JOIN Images AS i ON i.ProductId = p.Id";

            var products = await Connection.QueryAsync<ProductGetBaseDto>(sql, null, Transaction);
            return products.ToList();
        }

        public async Task<List<ProductInventoryGetDto>> GetAllInventoryProducts()
        {
            var sql = @"SELECT p.Id, p.Code, p.FullName, c.CategoryName, p.SaleQty, p.CombinedQty
                        FROM
                        Products AS p
                        JOIN Categories AS c ON p.CategoryId = c.Id";

            var products = await Connection.QueryAsync<ProductInventoryGetDto>(sql, null, Transaction);

            return products.ToList();
        }

        public async Task<ProductDetailDto> GetProductDetail(int id)
        {
            var sql = @"SELECT p.Id, p.FullName, p.Price, c.CategoryName, p.SaleQty, p.Description, i.ImageUrl
                        FROM
                        Products AS p
                        JOIN Categories AS c ON p.CategoryId  = c.Id
                        LEFT JOIN Images AS i ON i.ProductId = p.Id
                        WHERE p.Id = @id";

            var productDetail = await Connection.QueryAsync<ProductDetailDto>(sql, new { id}, Transaction);

            return productDetail.FirstOrDefault();
        }
    }
}
