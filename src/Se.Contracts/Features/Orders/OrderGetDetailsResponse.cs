using Se.Contracts.Shared.Crud.GetDetails;

namespace Se.Contracts.Features.Orders;

public record OrderGetDetailsResponse : GetDetailsResponseBase
{
    public string Note { get; set; } = "";
    public List<OrderItemDto> Items { get; set; } = [];
    
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public decimal Quantity { get; set; } = 1M;
    }
}
