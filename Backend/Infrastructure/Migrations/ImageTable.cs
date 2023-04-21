using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Migrations
{
    [Migration(202304131715)]
    public class ImageTable : Migration
    {
        public override void Up()
        {
            Create.Table("Image")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity().NotNullable()
                .WithColumn("PublicId").AsString(100).NotNullable()
                .WithColumn("ProductId").AsInt64().NotNullable();

            Create.ForeignKey()
                .FromTable("Image").ForeignColumn("ProductId")
                .ToTable("Product").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("Image");
        }
    }
}
