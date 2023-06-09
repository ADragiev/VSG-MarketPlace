﻿
using FluentMigrator;

namespace Infrastructure.Migrations.InitialMigrations
{
    [Migration(202304131803)]
    public class CreateOrderTable : Migration
    {
        public override void Up()
        {
            Create.Table("Order")
              .WithColumn("Id").AsInt64().PrimaryKey().Identity().NotNullable()
              .WithColumn("ProductCode").AsString(50).NotNullable()
              .WithColumn("ProductName").AsString(100).NotNullable()
              .WithColumn("Price").AsDecimal(19, 2).NotNullable()
              .WithColumn("Qty").AsInt64().NotNullable()
              .WithColumn("OrderedBy").AsString(50).NotNullable()
              .WithColumn("Date").AsDateTime().NotNullable()
              .WithColumn("Status").AsInt64().NotNullable()
              .WithColumn("ProductId").AsInt64().NotNullable();

            Create.ForeignKey()
                .FromTable("Order").ForeignColumn("ProductId")
                .ToTable("Product").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("Order");
        }
    }
}
