using Se.Domain.Shared.Entities;
using Se.Domain.Shared.ValueObjects;

namespace Se.Domain.Features.Orders;

public class OrderEntity : Entity
{
    private readonly List<OrderItemEntity> _items = [];
    
    public DateTime CreatedAtUtc { get; init; } = DateTime.UtcNow;
    public string Note { get; set; } = "";
    
    public IReadOnlyCollection<OrderItemEntity> Items => _items;

    public Guid AddItem(int productId, Quantity quantity)
    {
        var item = new OrderItemEntity
        {
            ProductId = productId,
            Quantity = quantity
        };
        
        _items.Add(item);
        
        return item.Guid;
    }

    public void DeleteItem(Guid guid)
    {
        var item = _items.FirstOrDefault(x => x.Guid == guid);
        
        if (item != null) 
            _items.Remove(item);
    }
}
