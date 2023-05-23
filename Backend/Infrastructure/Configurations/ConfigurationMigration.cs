using CloudinaryDotNet;
using FluentMigrator.Runner;
using Infrastructure.Migrations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public static class ConfigurationMigration
    {
        public static IServiceCollection AddConfigurationMigration(this IServiceCollection serviceCollection, IConfiguration config)
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder(config.GetConnectionString("DefaultConnection"));
            connectionStringBuilder.TrustServerCertificate = true;

            serviceCollection
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddSqlServer()
                .WithGlobalConnectionString(connectionStringBuilder.ConnectionString)
                .ScanIn(typeof(CategoryTable).Assembly).For.Migrations());

            serviceCollection.AddSingleton<Database>();
            serviceCollection.AddSingleton<ViewsAndFunctions>();

            return serviceCollection;
        }

        public static void CreateDatabase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var database = scope.ServiceProvider.GetRequiredService<Database>();
            database.CreateDatabase();
        }

        public static void CreateTables(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }

        public static void CreateViewsAndFunctions(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var views = scope.ServiceProvider.GetRequiredService<ViewsAndFunctions>();
            views.CreateViewsAndFunctions();
        }
    }
}
