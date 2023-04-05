using Application.Models.GenericRepo;
using Application.Models.OrderModels.Dtos;
using Application.Models.OrderModels.Interfaces;
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
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(IMarketPlaceContext marketPlaceContext)
            : base(marketPlaceContext)
        {
        }

        public List<OrderPendingDto> GetAllPendingOrders()
        {
            var sql = @"SELECT o.Id AS OrderCode, p.Id AS ProductCode, o.Qty, (o.Qty * p.Price) AS Price, o.OrderedBy, o.OrderDate
                        FROM Orders AS o
                        JOIN Products AS p ON o.ProductCode = p.Id
                        WHERE OrderStatus = 0";

            return Connection.Query<OrderPendingDto>(sql, null, Transaction).ToList();
        }

        public List<OrderGetMineDto> GetMyOrders(string email)
        {
            var sql = @"SELECT o.Id AS OrderCode, p.FullName AS ProductName, o.Qty, (o.Qty * p.Price) AS Price, o.OrderDate, o.OrderStatus
                        FROM Orders AS o
                        JOIN Products AS p ON o.ProductCode = p.Id
                        WHERE OrderedBy = @email";

            return Connection.Query<OrderGetMineDto>(sql, new { email }, Transaction).ToList();
        }
    }
}
