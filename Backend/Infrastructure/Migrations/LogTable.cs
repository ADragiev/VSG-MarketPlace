using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Migrations
{
    [Migration(202304181021)]
    public class LogTable : Migration
    {
        public override void Up()
        {
            Create.Table("Log")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity().NotNullable()
            .WithColumn("CreatedOn").AsDateTime().NotNullable()
            .WithColumn("Message").AsString().NotNullable()
            .WithColumn("Level").AsString(10).NotNullable()
            .WithColumn("StackTrace").AsString().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Log");
        }
    }
}
