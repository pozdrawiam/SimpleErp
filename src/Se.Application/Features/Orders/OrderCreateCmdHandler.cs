using Se.Domain.Features.Orders;

namespace Se.Application.Features.Orders;

public class OrderCreateCmdHandler
{
    public Task<int> Handle(OrderCreateCmd cmd)
    {
        var entity = new OrderEntity();
        
        entity.AddItem(1, 123);
        
        return Task.FromResult(entity.Id);
    }
}
