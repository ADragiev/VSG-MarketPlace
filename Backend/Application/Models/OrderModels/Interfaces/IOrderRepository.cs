using Application.Models.GenericRepo;
using Application.Models.OrderModels.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.OrderModels.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<List<OrderPendingDto>> GetAllPendingOrders();
        Task<List<OrderGetMineDto>> GetMyOrders(string email);
    }
}
