using Application.Models.OrderModels.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.OrderModels.Interfaces
{
    public interface IOrderService
    {
        Task<OrderGetDto> Create(OrderCreateDto dto);

        Task<List<OrderPendingDto>> GetAllPendingOrders();
        Task<List<OrderGetMineWithStringStatusDto>> GetMyOrders(string email);

        Task CompleteOrder(int id);

        Task RejectOrder(int id);
    }
}
