using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Migrations.JuneMigrations
{
    [Migration(202306051011)]
    public class CreateLendedItemTable : Migration
    {
        public override void Down()
        {
            Delete.Table("LendedItem");
        }

        public override void Up()
        {
            Create.Table("LendedItem")
              .WithColumn("Id").AsInt64().PrimaryKey().Identity().NotNullable()
              .WithColumn("Qty").AsInt64().NotNullable()
              .WithColumn("LendedBy").AsString(50).NotNullable()
              .WithColumn("StartDate").AsDateTime().NotNullable()
              .WithColumn("EndDate").AsDateTime().Nullable().WithDefaultValue(null)
              .WithColumn("Status").AsInt64().NotNullable()
              .WithColumn("ProductId").AsInt64().NotNullable();

            Create.ForeignKey()
                .FromTable("LendedItem").ForeignColumn("ProductId")
                .ToTable("Product").PrimaryColumn("Id");
        }
    }
}
