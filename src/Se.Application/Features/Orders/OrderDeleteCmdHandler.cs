namespace Se.Application.Features.Orders;

public class OrderDeleteCmdHandler
{
    private readonly IOrderRepo _orderRepo;

    public OrderDeleteCmdHandler(IOrderRepo orderRepo)
    {
        _orderRepo = orderRepo;
    }
    
    public async Task<int> Handle(OrderDeleteCmd cmd)
    {
        var order = await _orderRepo.GetAsync(cmd.Id);
        
        if (order == null)
            return 0;

        await _orderRepo.DeleteManyAsync([cmd.Id]);
        
        return order.Id;
    }
}
