using FluentMigrator;
using FluentMigrator.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Migrations.JuneMigrations
{
    [Migration(202306070914)]
    public class AddUniqueIndexOnCodeAndLocation : Migration
    {
        public override void Down()
        {
            Delete.Index("IX_Product_Code_Location").OnTable("Product");
        }

        public override void Up()
        {
            Create.Index("IX_Product_Code_Location")
             .OnTable("Product").InSchema("dbo")
             .OnColumn("Code").Ascending()
             .OnColumn("LocationId").Ascending()
             .WithOptions().Unique()
             .WithOptions().Filter("IsDeleted = 0");
        }
    }
}
