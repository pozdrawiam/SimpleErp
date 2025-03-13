using Se.Contracts.Shared.Cqs;

namespace Se.Contracts.Features.Orders;

public class OrderUpdateRequest : ICmd
{
    public int Id { get; set; }
    public string Note { get; set; } = "";
    public List<OrderItemDto> Items { get; set; } = [];
}
