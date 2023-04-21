using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.GenericRepo
{
    public interface IGenericRepository<T>
    {
        Task<List<T>> AllAsync();

        Task<T> GetByIdAsync(int id);

        Task<int> CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(int id);

        Task SetFieldAsync(int id, string fieldName, object value);
    }
}
