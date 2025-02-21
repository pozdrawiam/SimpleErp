namespace Se.Domain.Features.Orders;

public class OrderEntity
{
    private readonly List<OrderItemEntity> _items = [];
    
    public int Id { get; init; }
    public DateTime CreatedAtUtc { get; init; } = DateTime.UtcNow;
    public string Note { get; set; } = "";
    
    public IReadOnlyCollection<OrderItemEntity> Items => _items;

    public Guid AddItem(int productId, decimal quantity)
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

public class OrderItemEntity
{
    public int Id { get; init; }
    public Guid Guid { get; } = Guid.NewGuid();
    public required int ProductId { get; init; }
    public Quantity Quantity { get; set; } = 1M;
}

public abstract class ValueObject<TValue>
{
    protected ValueObject(TValue value)
    {
        Value = value;
    }

    public TValue Value { get; }

    public static implicit operator TValue(ValueObject<TValue> obj) => obj.Value;
}

public class Quantity : ValueObject<decimal>
{
    public Quantity(decimal value) : base(value)
    {
        if (value < 0.000001M)
            throw new ArgumentOutOfRangeException(nameof(value));
    }

    public static implicit operator Quantity(decimal value) => new(value);
}

