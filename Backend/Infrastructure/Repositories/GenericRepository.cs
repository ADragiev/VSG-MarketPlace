using Application.Models.GenericRepo;
using Dapper;
using Infrastructure.Repositories.GenericRepository.Context;
using System.Data;


namespace Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
    {
        private readonly IMarketPlaceContext marketPlaceContext;
        public GenericRepository(IMarketPlaceContext marketPlaceContext)
        {
            this.marketPlaceContext = marketPlaceContext;
        }

        public IDbConnection Connection => marketPlaceContext.Connection;
        public IDbTransaction Transaction => marketPlaceContext.Transaction;

        public async Task<int> CreateAsync(T entity)
        {
            return (int)await Connection.InsertAsync<T>(entity, Transaction);
            //Is it good to cast like this
        }

        public async Task DeleteAsync(int id)
        {
            await Connection.DeleteAsync<T>(id, Transaction);
        }

        public async Task<List<T>> AllAsync()
        {
            return (await Connection.GetListAsync<T>(null, null, Transaction)).ToList();
        }

        public async Task<T> GetByIdAsync(int CategoryId)
        {
            return await Connection.GetAsync<T>(CategoryId, Transaction);
        }

        public async Task SetFieldAsync(int id, string fieldName, object value)
        {
            var tableName = typeof(T).Name;

            var sql = @$"UPDATE [{tableName}]
                      SET {fieldName} = @value
                        WHERE Id = @id";

            await Connection.ExecuteAsync(sql, new { value, id }, Transaction);
        }

        public async Task UpdateAsync(T entity)
        {
            await Connection.UpdateAsync<T>(entity, Transaction);
        }
    }
}
