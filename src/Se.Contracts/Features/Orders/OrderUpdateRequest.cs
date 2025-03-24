using Se.Contracts.Shared.Crud.Update;

namespace Se.Contracts.Features.Orders;

public record OrderUpdateRequest : UpdateRequestBase, ICmd
{
    public OrderUpdateRequest(int Id) : base(Id)
    {
    }

    public string Note { get; set; } = "";
    public List<OrderItemDto> Items { get; set; } = [];
    
    public class OrderItemDto
    {
        public Guid Guid { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; } = 1M;
    }
}
