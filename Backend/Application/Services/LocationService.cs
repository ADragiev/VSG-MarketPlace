using Application.Models.Cache;
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
        private const string locationsKey = "locations";

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
            //var cachedLocations = await cacheService.GetData<List<LocationGetDto>>(locationsKey);

            //if(cachedLocations != null)
            //{
            //    return cachedLocations;
            //}

            var locations = await locationRepo.AllAsync();
            var locationsDto = mapper.Map<List<LocationGetDto>>(locations);

            //await cacheService.SetData(locationsKey, locationsDto, DateTimeOffset.Now.AddMinutes(30));

            return locationsDto;
        }
    }
}
