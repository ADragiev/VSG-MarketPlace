using Dapper;
using Infrastructure.Contracts;
using Infrastructure.Repositories.GenericRepository.Context;
using System.Data;

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

        public T GetByID(int CategoryId)
        {
            return Connection.Get<T>(CategoryId);
        }

        public void Update(T entity)
        {
             Connection.Update<T>(entity);
        }
    }
}
