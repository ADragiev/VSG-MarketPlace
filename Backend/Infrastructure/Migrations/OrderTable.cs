using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Migrations
{
    public class OrderTable : Migration
    {
        public override void Up()
        {
            Create.Table("Order")
              .WithColumn("Id").AsInt64().PrimaryKey().Identity().NotNullable()
              .WithColumn("Qty").AsInt64().NotNullable()
              .WithColumn("OrderedBy").AsString(50).NotNullable()
              .WithColumn("OrderDate").AsDateTime().NotNullable()
              .WithColumn("OrderStatus").AsInt64().NotNullable()
              .WithColumn("ProductId").AsInt64().NotNullable();

            Create.ForeignKey()
                .FromTable("Order").ForeignColumn("ProductId")
                .ToTable("Product").PrimaryColumn("Id");
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
