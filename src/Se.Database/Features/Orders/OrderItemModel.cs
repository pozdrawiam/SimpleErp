namespace Se.Database.Features.Orders;

public class OrderItemModel
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Guid Guid { get; set; }
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }
}
