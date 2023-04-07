using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.GenericRepo
{
    public interface IGenericRepository<T>
    {
        Task<List<T>> GetAll();

        Task<T> GetByID(int id);

        Task<int> Create(T entity);

        Task Update(T entity);

        Task Delete(int id);

        Task SetField(int id, string fieldName, object value);
    }
}
