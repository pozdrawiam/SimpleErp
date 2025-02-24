namespace Se.Contracts.Features.Orders;

public class OrderCreateRequest
{
    public string Note { get; set; } = "";
    public List<OrderItemDto> Items { get; set; } = [];
}
