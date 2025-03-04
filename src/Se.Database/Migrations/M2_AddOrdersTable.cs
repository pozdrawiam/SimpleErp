using FluentMigrator;

namespace Se.Database.Migrations;

[Migration(2)]
public class M2_AddOrdersTable : Migration
{
    public override void Up()
    {
        Create.Table("Orders")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("CreatedAtUtc").AsDateTime()
            .WithColumn("Note").AsString().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Orders");
    }
}
