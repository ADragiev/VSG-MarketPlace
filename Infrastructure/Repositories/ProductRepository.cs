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
        public ProductRepository(MarketPlaceContext marketPlaceContext)
            : base(marketPlaceContext)
        {
        }

        public List<ProductGetBaseDto> GetAllIndexProducts()
        {
            var sql = @"SELECT p.Id AS Code, c.CategoryName AS Category, p.Price, p.SaleQty, i.ImageUrl AS DefaultImage
                        FROM 
                        Products AS p 
                        JOIN Categories AS c ON p.CategoryId = c.Id 
                        JOIN Images AS i ON i.ProductCode = p.Id 
                        WHERE i.IsDefault = 1";

            return Connection.Query<ProductGetBaseDto>(sql).ToList();
        }

        public List<ProductInventoryGetDto> GetAllInventoryProducts()
        {
            var sql = @"SELECT p.Id AS Code, p.FullName, c.CategoryName, p.SaleQty, p.CombinedQty
                        FROM
                        Products AS p
                        JOIN Categories AS c ON p.CategoryId = c.Id";

            var products = Connection.Query<ProductInventoryGetDto>(sql).ToList();

            return products;
        }

        public ProductDetailDto GetProductDetail(int id)
        {
            var sql = @"SELECT p.Id, p.FullName, p.Price, c.CategoryName, p.SaleQty, p.Description, i.ImageUrl
                        FROM
                        Products AS p
                        JOIN Categories AS c ON p.CategoryId  = c.Id
                        JOIN Images AS i ON i.ProductCode = p.Id
                        WHERE p.Id = @id";
            //Sql Injection 

            var products = Connection.Query<ProductDetailDto, ImageProductDetailsDto, ProductDetailDto>(sql, (product, image) =>
            {
                product.Images.Add(image);
                return product;
            }, new { id }, splitOn: "ImageUrl");

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
