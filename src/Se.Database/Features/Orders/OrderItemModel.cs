using System.ComponentModel.DataAnnotations.Schema;

namespace Se.Database.Features.Orders;

[Table("OrderItems")]
public class OrderItemModel
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Guid Guid { get; set; }
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }
}
