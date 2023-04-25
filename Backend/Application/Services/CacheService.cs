using Application.Models.Cache;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Text.Json;

namespace Application.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase redisDb;

        public CacheService(IConfiguration config)
        {
            var redis = ConnectionMultiplexer.Connect(config.GetValue<string>("Redis:Connection"));
            redisDb = redis.GetDatabase();
        }

        public async Task<T> GetData<T>(string key)
        {
            var value = await redisDb.StringGetAsync(key);

            if (!String.IsNullOrEmpty(value))
            {
                return JsonSerializer.Deserialize<T>(value);
            }

            return default;
        }

        public async Task<bool> RemoveData(string key)
        {
            var keyExists = await redisDb.KeyExistsAsync(key);

            if (keyExists)
            {
                return await redisDb.KeyDeleteAsync(key);
            }
            return false;
        }

        public async Task<bool> SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            var isSet = await redisDb.StringSetAsync(key, JsonSerializer.Serialize(value), expiryTime);

            return isSet;
        }
    }
}
