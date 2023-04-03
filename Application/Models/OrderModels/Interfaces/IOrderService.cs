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
        OrderGetDto Create(OrderCreateDto dto);

        List<OrderPendingDto> GetAllPendingOrders();
        List<OrderGetMineDto> GetMyOrders(string email);
    }
}
