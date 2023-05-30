using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.CacheModels.Interfaces
{
    public interface ICacheService
    {
        Task<T> GetDataAsync<T>(string cacheKey);

        Task SetDataAsync<T>(string cacheKey, T value, TimeSpan time);
    }
}
