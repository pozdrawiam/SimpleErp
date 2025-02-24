using Se.Application.Shared;
using Se.Contracts.Features.Orders;
using Se.Domain.Features.Orders;

namespace Se.Application.Features.Orders;

public class OrderCreateCmdHandler : ICmdHandler<OrderCreateRequest>
{
    private readonly IOrderRepo _repo;

    public OrderCreateCmdHandler(IOrderRepo repo)
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
            entity.AddItem(item.ProductId, item.Quantity);
        
        var id = await _repo.AddAsync(entity);

        return id;
    }
}
