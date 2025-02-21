using Se.Domain.Shared.ValueObjects;

namespace Se.Domain.Features.Orders;

public class OrderItemEntity
{
    public int Id { get; init; }
    public Guid Guid { get; } = Guid.NewGuid();
    public required int ProductId { get; init; }
    public Quantity Quantity { get; set; } = 1M;
}