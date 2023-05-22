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
            var sql = @"SELECT * FROM PendingOrders";

            var pendingOrders = await connection.QueryAsync<OrderPendingDto>(sql, null, transaction);
            return pendingOrders.ToList();
        }

        public async Task<List<OrderGetMineDto>> GetMyOrdersAsync(string email)
        {
            var sql = @"SELECT * FROM GetMyOrdersViewSql(@email)";

            var orders = await connection.QueryAsync<OrderGetMineDto>(sql, new { email }, transaction);
            return orders.ToList();
        }
    }
}
