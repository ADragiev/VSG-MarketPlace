using Application.Models.GenericRepo;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace Infrastructure.Repositories.GenericRepository.Context
{
    public class MarketPlaceContext : IMarketPlaceContext
    {
        private IDbConnection connection;
        private IDbTransaction transaction;

        public MarketPlaceContext(IConfiguration config)
        {
            connection = new SqlConnection(config.GetConnectionString("DefaultConnection"));
            connection.Open();

            transaction = connection.BeginTransaction();
        }

        public IDbConnection Connection => connection;

        public IDbTransaction Transaction => transaction;

        public void Dispose()
        {
            if (connection != null)
            {
                connection.Close();
                connection.Dispose();
            }
            if (transaction != null)
            {
                transaction.Dispose();
            }
        }
    }
}
