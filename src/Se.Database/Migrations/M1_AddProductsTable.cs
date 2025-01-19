using FluentMigrator;

namespace Se.Database.Migrations;

[Migration(1)]
public class M1_AddProductsTable : Migration
{
    public override void Up()
    {
        Create.Table("Products")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString(255).NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Products");
    }
}
