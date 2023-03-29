using Dapper;
using Infrastructure.Contracts;
using Infrastructure.Repositories.GenericRepository.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
    {
        private readonly MarketPlaceContext marketPlaceContext;
        public GenericRepository(MarketPlaceContext marketPlaceContext)
        {
            this.marketPlaceContext = marketPlaceContext;
        }

        public IDbConnection Connection => marketPlaceContext.Connection;

        public int Create(T entity)
        {
            return (int)Connection.Insert<T>(entity);
        }

        public void Delete(int id)
        {
            Connection.Delete<T>(id);
        }

        public List<T> GetAll()
        {
            return Connection.GetList<T>().ToList();
        }

        public T GetByID(int id)
        {
            return Connection.Get<T>(id);
        }

        public void Update(T entity)
        {
             Connection.Update<T>(entity);
        }
    }
}
