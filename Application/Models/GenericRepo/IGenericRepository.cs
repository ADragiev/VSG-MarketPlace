using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.GenericRepo
{
    public interface IGenericRepository<T>
    {
        List<T> GetAll();

        T GetByID(int id);

        int Create(T entity);

        void Update(T entity);

        void Delete(int id);
    }
}
