using Application.Helpers.Constants;
using Application.Models.LentItemModels.Dtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers.Profiles
{
    public class LentItemProfile : Profile
    {
        public LentItemProfile()
        {
            CreateMap<LentItemCreateDto, LentItem>();

            CreateMap<LentItemGetDto, LentItemWithoutUserGetDto>()
                .ForMember(dest => dest.StartDate, src => src.MapFrom(src => TimeZoneInfo.ConvertTime(src.StartDate, TimeZoneInfo.FindSystemTimeZoneById(DateFormatConstants.EasternEuropeTimeZone)).ToString(DateFormatConstants.DefaultDateFormat)))
                .ForMember(dest => dest.EndDate, src => src.MapFrom(src => src.EndDate != null ? TimeZoneInfo.ConvertTime(src.EndDate.GetValueOrDefault(), TimeZoneInfo.FindSystemTimeZoneById(DateFormatConstants.EasternEuropeTimeZone)).ToString(DateFormatConstants.DefaultDateFormat) : null));

            CreateMap<LentItemGetDto, LentItemGetMineDto>()
               .ForMember(dest => dest.StartDate, src => src.MapFrom(src => TimeZoneInfo.ConvertTime(src.StartDate, TimeZoneInfo.FindSystemTimeZoneById(DateFormatConstants.EasternEuropeTimeZone)).ToString(DateFormatConstants.DefaultDateFormat)))
               .ForMember(dest => dest.EndDate, src => src.MapFrom(src => src.EndDate != null ? TimeZoneInfo.ConvertTime(src.EndDate.GetValueOrDefault(), TimeZoneInfo.FindSystemTimeZoneById(DateFormatConstants.EasternEuropeTimeZone)).ToString(DateFormatConstants.DefaultDateFormat) : null));


        }
    }
}
