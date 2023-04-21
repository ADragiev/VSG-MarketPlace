using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Migrations
{
    [Migration(202304131656)]
    public class CategoryTable : Migration
    {
        public override void Up()
        {
            Create.Table("Category")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(50).NotNullable();

            Insert.IntoTable("Category").Row(new { Name = "Laptops" });
            Insert.IntoTable("Category").Row(new { Name = "Monitors" });
            Insert.IntoTable("Category").Row(new { Name = "Chairs" });
            Insert.IntoTable("Category").Row(new { Name = "Keyboards" });
            Insert.IntoTable("Category").Row(new { Name = "Mouses" });
            Insert.IntoTable("Category").Row(new { Name = "Mouse pads" });
        }

        public override void Down()
        {
            Delete.Table("Category");
        }
    }
}
