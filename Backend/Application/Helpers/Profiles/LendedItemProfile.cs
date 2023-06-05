using Application.Helpers.Constants;
using Application.Models.LendedItemModels.Dtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers.Profiles
{
    public class LendedItemProfile : Profile
    {
        public LendedItemProfile()
        {
            CreateMap<LendedItemCreateDto, LendedItem>();

            CreateMap<LendedItemGetDto, LendedItemForGroupGetDto>()
                .ForMember(dest => dest.StartDate, src => src.MapFrom(src => TimeZoneInfo.ConvertTime(src.StartDate, TimeZoneInfo.FindSystemTimeZoneById(DateFormatConstants.EasternEuropeTimeZone)).ToString(DateFormatConstants.DefaultDateFormat)))
                .ForMember(dest => dest.EndDate, src => src.MapFrom(src => src.EndDate != null ? TimeZoneInfo.ConvertTime(src.EndDate.GetValueOrDefault(), TimeZoneInfo.FindSystemTimeZoneById(DateFormatConstants.EasternEuropeTimeZone)).ToString(DateFormatConstants.DefaultDateFormat) : null));


        }
    }
}
