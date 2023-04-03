using Application.Models.GenericRepo;
using Dapper;
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

        public void SetField(int id, string fieldName, object value)
        {
            var tableAttribute =
                (System.ComponentModel.DataAnnotations.Schema.TableAttribute)typeof(T)
                    .GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.Schema.TableAttribute), false)
                    .FirstOrDefault();

            var tableName = tableAttribute.Name;

            var sql = @"UPDATE @tableName
                      SET @fieldName = @value
                        WHERE Id = @id";

            Connection.Execute(sql, new { tableName, fieldName, value, id });
        }

        public void Update(T entity)
        {
             Connection.Update<T>(entity);
        }
    }
}
