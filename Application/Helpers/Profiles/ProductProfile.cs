using Application.Models.ProductModels.Dtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductGetDto>()
                .ForMember(dest => dest.Code, src => src.MapFrom(src => src.Id));

            CreateMap<ProductCreateDto, Product>();
        }
    }
}
