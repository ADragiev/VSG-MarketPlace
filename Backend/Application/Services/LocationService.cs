using Application.Models.LocationModels.Dtos;
using Application.Models.LocationModels.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository locationRepo;
        private readonly IMapper mapper;

        public LocationService(ILocationRepository locationRepo,
            IMapper mapper)
        {
            this.locationRepo = locationRepo;
            this.mapper = mapper;
        }

        public async Task<List<LocationGetDto>> AllAsync()
        {
            var locations = await locationRepo.AllAsync();
            return mapper.Map<List<LocationGetDto>>(locations);
        }
    }
}
