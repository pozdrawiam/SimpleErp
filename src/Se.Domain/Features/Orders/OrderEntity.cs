namespace Se.Domain.Features.Orders;

public class OrderEntity
{
    private readonly List<OrderItem> _items = [];
    
    public int Id { get; init; }
    public DateTime CreatedAtUtc { get; init; } = DateTime.UtcNow;
    public string Note { get; set; } = "";
    
    public IReadOnlyCollection<OrderItem> Items => _items;

    public Guid AddItem(int productId, decimal quantity)
    {
        var item = new OrderItem
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

public class OrderItem
{
    public int Id { get; init; }
    public Guid Guid { get; } = Guid.NewGuid();
    public required int ProductId { get; init; }
    public Quantity Quantity { get; set; } = 1;
}

public class Quantity
{
    public Quantity(decimal value)
    {
        if (value < 0.000001M)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }
        
        Value = value;
    }

    public decimal Value { get; }
    
    public static implicit operator decimal(Quantity quantity) => quantity.Value;
    public static implicit operator Quantity(decimal value) => new(value);
}
