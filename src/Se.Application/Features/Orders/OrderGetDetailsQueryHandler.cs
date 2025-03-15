using Se.Application.Shared;
using Se.Contracts.Features.Orders;
using Se.Domain.Features.Orders;

namespace Se.Application.Features.Orders;

public class OrderGetDetailsQueryHandler : IQueryHandler<OrderGetDetailsRequest, OrderGetDetailsResponse>
{
    private readonly ICrudRepo<OrderEntity> _repo;

    public OrderGetDetailsQueryHandler(ICrudRepo<OrderEntity> repo)
    {
        _repo = repo;
    }
    
    public async Task<OrderGetDetailsResponse> Handle(OrderGetDetailsRequest query, CancellationToken _)
    {
        var entity = await _repo.GetAsync(query.Id);
        
        if (entity == null)
            throw new InvalidOperationException("Order not found");
        
        var response = new OrderGetDetailsResponse
        {
            Note = entity.Note,
            Items = entity.Items.Select(x => new OrderGetDetailsResponse.OrderItemDto
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity
            }).ToList()
        };
        
        return response;
    }
}
