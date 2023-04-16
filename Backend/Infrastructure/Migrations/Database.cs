using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Migrations
{
    public class Database
    {
        private readonly string masterConnectionString;

        public Database(IConfiguration config)
        {
            masterConnectionString = config.GetConnectionString("MasterConnection");
        }
        public void CreateDatabase()
        {
            var query = "SELECT * FROM sys.databases WHERE name = 'MarketPlace'";

            using (var connection = new SqlConnection(masterConnectionString))
            {
                var records = connection.Query(query);
                if (!records.Any())
                    connection.Execute($"CREATE DATABASE MarketPlace");
            }
        }
    }
}
