using FluentMigrator;

namespace Se.Database.Migrations;

[Migration(3)]
public class M3_AddOrderItemsTable : Migration
{
    public override void Up()
    {
        Create.Table("OrderItems")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("OrderId").AsInt32().ForeignKey("Orders", "Id")
            .WithColumn("Guid").AsGuid()
            .WithColumn("ProductId").AsInt32().ForeignKey("Products", "Id")
            .WithColumn("Quantity").AsDecimal();
    }

    public override void Down()
    {
        Delete.Table("OrderItems");
    }
}
