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
        }
    }
}
