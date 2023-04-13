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
                .WithColumn("CategoryName").AsString(50).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Category");
        }
    }
}
