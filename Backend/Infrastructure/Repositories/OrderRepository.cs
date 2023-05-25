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

        public async Task<List<Order>> GetAllPendingOrdersAsync()
        {
            var sql = @"SELECT * FROM [Order]
                        WHERE Status = 0";

            var pendingOrders = await connection.QueryAsync<Order>(sql, null, transaction);
            return pendingOrders.ToList();
        }

        public async Task<List<Order>> GetMyOrdersAsync(string user)
        {
            var sql = @"SELECT * FROM [Order]
                        WHERE OrderedBy = @user";

            var orders = await connection.QueryAsync<Order>(sql, new { user }, transaction);
            return orders.ToList();
        }
    }
}
