using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Cache
{
    public interface ICacheService
    {
        Task<T> GetData<T>(string key);

        Task<bool> SetData<T>(string key, T value, DateTimeOffset expirationTime);

        Task<object> RemoveData(string key);
    }
}
