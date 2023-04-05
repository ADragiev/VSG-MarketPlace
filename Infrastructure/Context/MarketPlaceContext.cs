using Application.Models.GenericRepo;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace Infrastructure.Repositories.GenericRepository.Context
{
    public class MarketPlaceContext : IMarketPlaceContext
    {
        private readonly IConfiguration config;
        private readonly string connectionString;
        private IDbConnection connection;
        private IDbTransaction transaction;

        public MarketPlaceContext(IConfiguration config)
        {
            this.config = config;
            connectionString = this.config.GetConnectionString("DefaultConnection");
            connection = new SqlConnection(connectionString);
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
