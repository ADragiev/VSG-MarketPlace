using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace Infrastructure.Repositories.GenericRepository.Context
{
    public class MarketPlaceContext : IDisposable
    {
        private readonly IConfiguration config;
        private readonly string connectionString;
        private readonly IDbConnection connection;

        public MarketPlaceContext(IConfiguration config)
        {
            this.config = config;
            connectionString = this.config.GetConnectionString("DefaultConnection");
            connection = new SqlConnection(connectionString);
        }

        public IDbConnection Connection => connection;

        public void Dispose()
        {
            connection.Close();
        }
    }
}
