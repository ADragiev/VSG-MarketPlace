using Application.Models.GenericRepo;
using Dapper;
using Infrastructure.Repositories.GenericRepository.Context;
using System.Data;


namespace Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
    {
        protected readonly IDbConnection connection;
        protected readonly IDbTransaction transaction;

        public GenericRepository(IMarketPlaceContext marketPlaceContext)
        {
            connection = marketPlaceContext.Connection;
            transaction = marketPlaceContext.Transaction;
        }

        public async Task<int> CreateAsync(T entity)
        {
            return (int)await connection.InsertAsync<T>(entity, transaction);
        }

        public async Task DeleteAsync(int id)
        {
            await connection.DeleteAsync<T>(id, transaction);
        }

        public async Task<List<T>> AllAsync()
        {
            return (await connection.GetListAsync<T>(null, null, transaction)).ToList();
        }

        public async Task<T> GetByIdAsync(int CategoryId)
        {
            return await connection.GetAsync<T>(CategoryId, transaction);
        }

        public async Task SetFieldAsync(int id, string fieldName, object value)
        {
            var tableName = typeof(T).Name;

            var sql = @$"UPDATE [{tableName}]
                      SET {fieldName} = @value
                        WHERE Id = @id";

            await connection.ExecuteAsync(sql, new { value, id }, transaction);
        }

        public async Task UpdateAsync(T entity)
        {
            await connection.UpdateAsync<T>(entity, transaction);
        }
    }
}
