using Application.Models.CategoryModels.Dtos;
using Application.Models.LocationModels.Dtos;
using Application.Models.LocationModels.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace Application.Services
{
    public class LocationService : ILocationService
    {
        private const string locationsKey = "locations-angel";

        private readonly ILocationRepository locationRepo;
        private readonly IMapper mapper;
        private readonly IDistributedCache cacheService;

        public LocationService(ILocationRepository locationRepo,
            IMapper mapper,
            IDistributedCache cacheService)
        {
            this.locationRepo = locationRepo;
            this.mapper = mapper;
            this.cacheService = cacheService;
        }

        public async Task<List<LocationGetDto>> AllAsync()
        {
            var cachedLocations = await cacheService.GetAsync(locationsKey);

            if (cachedLocations != null)
            {
                return JsonSerializer.Deserialize<List<LocationGetDto>>(Encoding.UTF8.GetString(cachedLocations));
            }

            var locations = await locationRepo.AllAsync();
            var locationsDto = mapper.Map<List<LocationGetDto>>(locations);

            await cacheService.SetAsync(locationsKey, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(locationsDto)),
                                                new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) });

            return locationsDto;
        }
    }
}
