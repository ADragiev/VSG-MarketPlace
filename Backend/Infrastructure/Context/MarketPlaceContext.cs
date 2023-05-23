using Application.Models.GenericRepo;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Runtime.CompilerServices;

namespace Infrastructure.Repositories.GenericRepository.Context
{
    public class MarketPlaceContext : IMarketPlaceContext
    {
        private IDbConnection connection;
        private IDbTransaction transaction;

        public MarketPlaceContext(IConfiguration config)
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder(config.GetConnectionString("DefaultConnection"));
            connectionStringBuilder.TrustServerCertificate = true;
            connection = new SqlConnection(connectionStringBuilder.ConnectionString);
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
