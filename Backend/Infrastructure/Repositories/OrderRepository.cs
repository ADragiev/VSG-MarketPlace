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

        public async Task<List<OrderPendingDto>> GetAllPendingOrders()
        {
            var sql = @"SELECT Id, ProductCode, Qty, Price, OrderedBy, OrderDate
                        FROM [Order]
                        WHERE OrderStatus = 0";

            var pendingOrders = await Connection.QueryAsync<OrderPendingDto>(sql, null, Transaction);
            return pendingOrders.ToList();
        }

        public async Task<List<OrderGetMineDto>> GetMyOrders(string email)
        {
            var sql = @"SELECT Id, ProductName, Qty, Price, OrderDate, OrderStatus
                        FROM [Order] AS o
                        WHERE OrderedBy = @email";

            var orders = await Connection.QueryAsync<OrderGetMineDto>(sql, new { email }, Transaction);
            return orders.ToList();
        }
    }
}
