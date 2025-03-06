using Se.Application.Shared;
using Se.Contracts.Features.Orders;
using Se.Contracts.Shared.Crud.QueryAll;
using Se.Domain.Features.Orders;

namespace Se.Application.Features.Orders;

public class OrdersAllQueryHandler : IQueryHandler<OrdersQueryAllRequest, QueryAllResponse>
{
    private readonly ICrudRepo<OrderEntity> _repo;

    public OrdersAllQueryHandler(ICrudRepo<OrderEntity> repo)
    {
        _repo = repo;
    }
    
    public async Task<QueryAllResponse> Handle(OrdersQueryAllRequest query, CancellationToken ct)
    {
        var result = await _repo.QueryAllAsync(query);
        var response = new QueryAllResponse(result.Data, result.TotalCount);
        
        return response;
    }
}
