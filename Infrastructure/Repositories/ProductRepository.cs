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

        public List<ProductGetBaseDto> GetAllProductBase()
        {
            var sql = @"SELECT p.Id AS Code, c.CategoryName AS Category, p.Price, p.SaleQty, i.ImageUrl AS DefaultImage
                        FROM 
                        Products AS p 
                        JOIN Categories AS c ON p.CategoryId = c.Id 
                        JOIN Images AS i ON i.ProductCode = p.Id 
                        WHERE i.IsDefault = 1";

            return Connection.Query<ProductGetBaseDto>(sql).ToList();
        }
    }
}
