using Se.Application.Shared;
using Se.Contracts.Features.Orders;
using Se.Domain.Features.Orders;
using Se.Domain.Shared.ValueObjects;

namespace Se.Application.Features.Orders;

public class OrderCreateCmdHandler : ICmdHandler<OrderCreateRequest>
{
    private readonly ICrudRepo<OrderEntity> _repo;

    public OrderCreateCmdHandler(ICrudRepo<OrderEntity> repo)
    {
        _repo = repo;
    }
    
    public async Task<int> Handle(OrderCreateRequest cmd, CancellationToken _)
    {
        var entity = new OrderEntity
        {
            Note = cmd.Note
        };
        
        foreach (var item in cmd.Items)
            entity.AddItem(item.ProductId, new Quantity(item.Quantity));
        
        var id = await _repo.AddAsync(entity);

        return id;
    }
}
