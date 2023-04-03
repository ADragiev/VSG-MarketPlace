using Application.Models.OrderModels.Interfaces;
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
        public OrderRepository(MarketPlaceContext marketPlaceContext)
            : base(marketPlaceContext)
        {
        }
    }
}
