using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories.GenericRepository.Context
{
    public class MarketPlaceContext
    {
        private readonly IConfiguration config;
        private readonly string connection;

        public MarketPlaceContext(IConfiguration config)
        {
            this.config = config;
            connection = this.config.GetConnectionString("DefaultConnection");
        }

        public IDbConnection Connection => new SqlConnection(connection);
    }
}
