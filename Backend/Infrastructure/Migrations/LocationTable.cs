using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Migrations
{
    [Migration(202304131657)]
    public class LocationTable : Migration
    {
        public override void Up()
        {
            Create.Table("Location")
                 .WithColumn("Id").AsInt64().PrimaryKey().Identity().NotNullable()
                 .WithColumn("Name").AsString(15).NotNullable();

            Insert.IntoTable("Location").Row(new { Name = "Plovdiv" });
            Insert.IntoTable("Location").Row(new { Name = "Tarnovo" });
            Insert.IntoTable("Location").Row(new { Name = "Home Office" });
        }

        public override void Down()
        {
            Delete.Table("Location");
        }
    }
}
