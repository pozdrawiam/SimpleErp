using Se.Contracts.Shared.Cqs;

namespace Se.Contracts.Features.Orders;

public class OrderCreateRequest : ICmd
{
    public string Note { get; set; } = "";
    public List<OrderItemDto> Items { get; set; } = [];
}
