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
            var sql = @"SELECT p.Id AS Code, c.CategoryName AS Category, p.Price, p.SaleQty, i.ImageUrl AS DefaultImage
                        FROM 
                        Products AS p 
                        JOIN Categories AS c ON p.CategoryId = c.Id 
                        JOIN Images AS i ON i.ProductCode = p.Id 
                        WHERE i.IsDefault = 1";

            var products = await Connection.QueryAsync<ProductGetBaseDto>(sql, null, Transaction);
            return products.ToList();
        }

        public async Task<List<ProductInventoryGetDto>> GetAllInventoryProducts()
        {
            var sql = @"SELECT p.Id AS Code, p.FullName, c.CategoryName, p.SaleQty, p.CombinedQty
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
                        LEFT JOIN Images AS i ON i.ProductCode = p.Id
                        WHERE p.Id = @id";

            var products = await Connection.QueryAsync<ProductDetailDto, ImageProductDetailsDto, ProductDetailDto>(sql, (product, image) =>
            {
                product.Images.Add(image);
                return product;
            }, new { id }, Transaction, splitOn: "ImageUrl");

            var result = products.GroupBy(p => p.FullName).Select(p =>
            {
                var groupedProduct = p.First();
                groupedProduct.Images = p.Select(p => p.Images.Single()).ToList();
                return groupedProduct;
            });

            return result.FirstOrDefault();
        }
    }
}
