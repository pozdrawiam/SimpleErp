using Se.Contracts.Shared.Cqs;
using Se.Contracts.Shared.Crud.Create;

namespace Se.Contracts.Features.Orders;

public record OrderCreateRequest : CreateRequestBase, ICmd
{
    public string Note { get; set; } = "";
    public List<OrderItemDto> Items { get; set; } = [];
    
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public decimal Quantity { get; set; } = 1M;
    }
}
