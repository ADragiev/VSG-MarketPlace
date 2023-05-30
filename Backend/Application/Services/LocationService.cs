using Application.Models.CacheModels.Interfaces;
using Application.Models.CategoryModels.Dtos;
using Application.Models.LocationModels.Dtos;
using Application.Models.LocationModels.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Application.Services
{
    public class LocationService : ILocationService
    {
        private const string locationsKey = "locations-angel";

        private readonly ILocationRepository locationRepo;
        private readonly IMapper mapper;
        private readonly ICacheService cacheService;

        public LocationService(ILocationRepository locationRepo,
            IMapper mapper,
            ICacheService cacheService)
        {
            this.locationRepo = locationRepo;
            this.mapper = mapper;
            this.cacheService = cacheService;
        }

        public async Task<List<LocationGetDto>> AllAsync()
        {
            var cachedLocations = await cacheService.GetDataAsync<List<LocationGetDto>>(locationsKey);

            if (cachedLocations != null)
            {
                return cachedLocations;
            }

            var locations = await locationRepo.AllAsync();
            var locationsDto = mapper.Map<List<LocationGetDto>>(locations);

            await cacheService.SetDataAsync(locationsKey, locationsDto, TimeSpan.FromMinutes(30));

            return locationsDto;
        }
    }
}
