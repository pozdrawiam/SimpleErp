using Se.Domain.Features.Orders;

namespace Se.Application.Features.Orders;

public class OrderCreateCmd
{
    public string Note { get; set; } = "";
    public List<OrderItemDto> Items { get; set; } = [];
}

public class OrderItemDto
{
    public int ProductId { get; set; }
    public Quantity Quantity { get; set; } = 1M;
}
