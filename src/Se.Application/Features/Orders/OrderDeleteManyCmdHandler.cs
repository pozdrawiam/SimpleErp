using Se.Application.Shared;
using Se.Contracts.Features.Orders;
using Se.Contracts.Shared.Crud.DeleteMany;
using Se.Domain.Features.Orders;

namespace Se.Application.Features.Orders;

public class OrderDeleteManyCmdHandler : ICmdHandler<OrderDeleteManyRequest>
{
    private readonly ICrudRepo<OrderEntity> _orderRepo;

    public OrderDeleteManyCmdHandler(ICrudRepo<OrderEntity> orderRepo)
    {
        _orderRepo = orderRepo;
    }
    
    public async Task<int> Handle(OrderDeleteManyRequest cmd, CancellationToken _)
    {
        if (cmd.Ids?.Count > 0)
            await _orderRepo.DeleteManyAsync(cmd.Ids);
        
        return 0;
    }
}
