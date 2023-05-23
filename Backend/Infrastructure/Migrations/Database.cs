using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Migrations
{
    public class Database
    {
        private readonly string masterConnectionString;
        private readonly string databaseName;

        public Database(IConfiguration config)
        {
            masterConnectionString = config.GetConnectionString("MasterConnection");

            var csb = new SqlConnectionStringBuilder(config.GetConnectionString("DefaultConnection"));
            this.databaseName = csb.InitialCatalog;
        }
        public void CreateDatabase()
        {
            var query = $"SELECT * FROM sys.databases WHERE name = @databaseName";

            using (var connection = new SqlConnection(masterConnectionString))
            {
                var records = connection.Query(query, new { databaseName });
                if (!records.Any())
                    connection.Execute($"CREATE DATABASE {databaseName}");
            }
        }
    }
}
