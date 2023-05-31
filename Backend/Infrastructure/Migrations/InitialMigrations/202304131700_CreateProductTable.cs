using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Migrations.InitialMigrations
{
    [Migration(202304131700)]
    public class CreateProductTable : Migration
    {
        public override void Up()
        {
            Create.Table("Product")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity().NotNullable()
                .WithColumn("Code").AsString(50).NotNullable()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("Price").AsDecimal(19, 2).NotNullable()
                .WithColumn("SaleQty").AsInt64().NotNullable()
                .WithColumn("CombinedQty").AsInt64().NotNullable()
                .WithColumn("Description").AsString(200).Nullable()
                .WithColumn("CategoryId").AsInt64().NotNullable()
                .WithColumn("LocationId").AsInt64().NotNullable();

            Create.UniqueConstraint("CodeAndLocation")
                  .OnTable("Product")
                  .Columns("Code", "LocationId");

            Create.ForeignKey()
                .FromTable("Product").ForeignColumn("CategoryId")
                .ToTable("Category").PrimaryColumn("Id");

            Create.ForeignKey()
                .FromTable("Product").ForeignColumn("LocationId")
                .ToTable("Location").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("Product");
        }
    }
}
