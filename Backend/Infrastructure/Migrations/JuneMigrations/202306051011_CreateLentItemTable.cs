using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Migrations.JuneMigrations
{
    [Migration(202306051011)]
    public class CreateLentItemTable : Migration
    {
        public override void Down()
        {
            Delete.Table("LentItem");
        }

        public override void Up()
        {
            Create.Table("LentItem")
              .WithColumn("Id").AsInt64().PrimaryKey().Identity().NotNullable()
              .WithColumn("Qty").AsInt64().NotNullable()
              .WithColumn("LentBy").AsString(50).NotNullable()
              .WithColumn("StartDate").AsDateTime().NotNullable()
              .WithColumn("EndDate").AsDateTime().Nullable().WithDefaultValue(null)
              .WithColumn("ProductId").AsInt64().NotNullable();

            Create.ForeignKey()
                .FromTable("LentItem").ForeignColumn("ProductId")
                .ToTable("Product").PrimaryColumn("Id");
        }
    }
}
