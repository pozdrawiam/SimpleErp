using Se.Domain.Features.Orders;

namespace Se.Application.Features.Orders;

public class OrderCreateCmdHandler
{
    private readonly IOrderRepo _repo;

    public OrderCreateCmdHandler(IOrderRepo repo)
    {
        _repo = repo;
    }
    
    public async Task<int> Handle(OrderCreateCmd cmd)
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
