using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Migrations.JuneMigrations
{
    [Migration(202306050920)]
    public class AddForLendQtyAndIsDeletedToProductTable : Migration
    {
        public override void Down()
        {
            Delete.Column("LendQty").FromTable("Product");
            Delete.Column("IsDeleted").FromTable("Product");
        }

        public override void Up()
        {
            Alter.Table("Product")
                .AddColumn("LendQty").AsInt64().NotNullable().WithDefaultValue(0)
                .AddColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false);
        }
    }
}
