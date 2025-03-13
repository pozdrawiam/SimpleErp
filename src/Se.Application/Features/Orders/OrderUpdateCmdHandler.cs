using Se.Application.Shared;
using Se.Contracts.Features.Orders;
using Se.Domain.Features.Orders;
using Se.Domain.Shared.ValueObjects;

namespace Se.Application.Features.Orders;

public class OrderUpdateCmdHandler : ICmdHandler<OrderUpdateRequest>
{
    private readonly ICrudRepo<OrderEntity> _repo;

    public OrderUpdateCmdHandler(ICrudRepo<OrderEntity> repo)
    {
        _repo = repo;
    }
    
    public async Task<int> Handle(OrderUpdateRequest cmd, CancellationToken _)
    {
        var entity = await _repo.GetAsync(cmd.Id) ??
                     throw new InvalidOperationException("Entity not found");

        entity.Note = cmd.Note;
        
        var requestItemGuids = cmd.Items.Select(i => i.Guid).ToHashSet();
        var itemsToRemove = entity.Items.Where(i => !requestItemGuids.Contains(i.Guid)).ToList();
        
        foreach (var item in itemsToRemove)
            entity.DeleteItem(item.Guid);
        
        foreach (var dto in cmd.Items)
        {
            var existingItem = entity.Items.FirstOrDefault(i => i.Guid == dto.Guid);
            
            if (existingItem != null)
                existingItem.Quantity = new Quantity(dto.Quantity);
            else
                entity.AddItem(dto.ProductId, new Quantity(dto.Quantity));
        }
    
        await _repo.UpdateAsync(entity);
        
        return entity.Id;
    }
}
