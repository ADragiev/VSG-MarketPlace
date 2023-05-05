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
        Task<OrderGetDto> CreateAsync(OrderCreateDto dto);

        Task<List<OrderPendingDto>> GetAllPendingOrdersAsync();
        Task<List<OrderGetMineDto>> GetMyOrdersAsync();

        Task CompleteOrderAsync(int id);

        Task RejectOrderAsync(int id);
    }
}
