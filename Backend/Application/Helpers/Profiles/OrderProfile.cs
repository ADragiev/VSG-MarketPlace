using Application.Models.OrderModels.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderCreateDto, Order>();
            CreateMap<Order, OrderGetDto>();
            CreateMap<OrderGetMineDto, OrderGetMineWithStringStatusDto>()
                .ForMember(dest => dest.OrderStatus, src => src.MapFrom(src => (OrderStatus)src.OrderStatus));
        }
    }
}
