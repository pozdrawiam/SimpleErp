using Se.Contracts.Shared.Crud.DeleteMany;

namespace Se.Application.Features.Orders;

public class OrderDeleteCmdHandler
{
    private readonly IOrderRepo _orderRepo;

    public OrderDeleteCmdHandler(IOrderRepo orderRepo)
    {
        _orderRepo = orderRepo;
    }
    
    public async Task<int> Handle(DeleteManyRequest cmd)
    {
        if (cmd.Ids?.Count > 0)
            await _orderRepo.DeleteManyAsync(cmd.Ids);
        
        return 0;
    }
}
