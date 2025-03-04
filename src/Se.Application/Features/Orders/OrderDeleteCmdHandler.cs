using Se.Application.Shared;
using Se.Contracts.Shared.Crud.DeleteMany;
using Se.Domain.Features.Orders;

namespace Se.Application.Features.Orders;

public class OrderDeleteManyCmdHandler : ICmdHandler<DeleteManyRequest>
{
    private readonly ICrudRepo<OrderEntity> _orderRepo;

    public OrderDeleteManyCmdHandler(ICrudRepo<OrderEntity> orderRepo)
    {
        _orderRepo = orderRepo;
    }
    
    public async Task<int> Handle(DeleteManyRequest cmd, CancellationToken _)
    {
        if (cmd.Ids?.Count > 0)
            await _orderRepo.DeleteManyAsync(cmd.Ids);
        
        return 0;
    }
}
