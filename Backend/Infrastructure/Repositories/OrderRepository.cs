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

        public async Task<List<OrderPendingDto>> GetAllPendingOrdersAsync()
        {
            var sql = @"SELECT Id, ProductCode, Qty, Price, OrderedBy, Date
                        FROM [Order]
                        WHERE Status = 0";

            var pendingOrders = await Connection.QueryAsync<OrderPendingDto>(sql, null, Transaction);
            return pendingOrders.ToList();
        }

        public async Task<List<OrderGetMineDto>> GetMyOrdersAsync(string email)
        {
            var sql = @"SELECT Id, ProductName, Qty, Price, Date, Status
                        FROM [Order]
                        WHERE OrderedBy = @email";

            var orders = await Connection.QueryAsync<OrderGetMineDto>(sql, new { email }, Transaction);
            return orders.ToList();
        }
    }
}
