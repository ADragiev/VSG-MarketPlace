using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Migrations.JuneMigrations
{
    [Migration(202306071249)]
    public class AddCheckConstraintForCombinedQtyToBeBiggerThanOtherTwo : Migration
    {
        public override void Down()
        {
            Execute.Sql("ALTER TABLE Product DROP CONSTRAINT CK_CombinedQtyBiggerThanOtherTwo_ProductTable");
        }

        public override void Up()
        {
            Execute.Sql("ALTER TABLE Product ADD CONSTRAINT CK_CombinedQtyBiggerThanOtherTwo_ProductTable CHECK (CombinedQty >= (LendQty + SaleQty))");
        }
    }
}
