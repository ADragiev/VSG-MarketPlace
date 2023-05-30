using Application.Models.CacheModels.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache cacheService;

        public CacheService(IDistributedCache cacheService)
        {
            this.cacheService = cacheService;
        }

        public async Task<T> GetDataAsync<T>(string cacheKey)
        {
            var cachedData = await cacheService.GetStringAsync(cacheKey);
            if (cachedData != null)
            {
                return JsonSerializer.Deserialize<T>(cachedData);
            }
            return default;
        }

        public async Task SetDataAsync<T>(string cacheKey, T value, TimeSpan time)
        {
            await cacheService.SetStringAsync(cacheKey, JsonSerializer.Serialize(value),
                                               new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = time });
        }
    }
}
