using Application.Helpers.Constants;
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
            CreateMap<Order, OrderGetDto>()
                .ForMember(dest=>dest.Date, src=>src.MapFrom(src=> src.Date.ToString(DateFormatConstants.DefaultDateFormat)));
        }
    }
}
