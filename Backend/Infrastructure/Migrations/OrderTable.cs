
using FluentMigrator;

namespace Infrastructure.Migrations
{
    [Migration(202304131803)]
    public class OrderTable : Migration
    {
        public override void Up()
        {
            Create.Table("Order")
              .WithColumn("Id").AsInt64().PrimaryKey().Identity().NotNullable()
              .WithColumn("ProductCode").AsString(50).NotNullable()
              .WithColumn("ProductName").AsString(100).NotNullable()
              .WithColumn("Price").AsDecimal(19,2).NotNullable()
              .WithColumn("Qty").AsInt64().NotNullable()
              .WithColumn("OrderedBy").AsString(50).NotNullable()
              .WithColumn("Date").AsDateTime().NotNullable()
              .WithColumn("Status").AsInt64().NotNullable()
              .WithColumn("ProductId").AsInt64().Nullable();

            Create.ForeignKey()
                .FromTable("Order").ForeignColumn("ProductId")
                .ToTable("Product").PrimaryColumn("Id")
                .OnDelete(System.Data.Rule.SetNull);
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
