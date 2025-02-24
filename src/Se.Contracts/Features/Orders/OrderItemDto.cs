namespace Se.Contracts.Features.Orders;

public class OrderItemDto
{
    public int ProductId { get; set; }
    public decimal Quantity { get; set; } = 1M;
}