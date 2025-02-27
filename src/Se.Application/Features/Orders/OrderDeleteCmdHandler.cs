using Se.Application.Shared;
using Se.Contracts.Shared.Crud.DeleteMany;

namespace Se.Application.Features.Orders;

public class OrderDeleteManyCmdHandler : ICmdHandler<DeleteManyRequest>
{
    private readonly IOrderRepo _orderRepo;

    public OrderDeleteManyCmdHandler(IOrderRepo orderRepo)
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
