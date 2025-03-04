using System.Transactions;
using Se.Application.Shared;
using Se.Database.Shared;
using Se.Domain.Features.Orders;

namespace Se.Database.Features.Orders;

public class OrderRepo : CrudDomainRepo<OrderModel, OrderEntity>
{
    private readonly ICrudRepo<OrderItemModel> _orderItemRepo;

    public OrderRepo(CrudRepo<OrderModel> crudRepo, ICrudRepo<OrderItemModel> orderItemRepo) : base(crudRepo)
    {
        _orderItemRepo = orderItemRepo;
    }
    
    public override async Task<int> AddAsync(OrderEntity entity)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        
        var orderId = await base.AddAsync(entity);
        
        foreach (var item in entity.Items)
        {
            var itemModel = new OrderItemModel
            {
                OrderId = orderId,
                Guid = item.Guid,
                ProductId = item.ProductId,
                Quantity = item.Quantity
            };
            
            await _orderItemRepo.AddAsync(itemModel);
        }

        return orderId;
    }

    protected override OrderModel MapEntityToModel(OrderEntity entity)
    {
        return new OrderModel
        {
            Id = entity.Id,
            CreatedAtUtc = entity.CreatedAtUtc,
            Note = entity.Note
        };
    }

    protected override OrderEntity MapModelToEntity(OrderModel model)
    {
        return new OrderEntity
        {
            Id = model.Id,
            CreatedAtUtc = model.CreatedAtUtc,
            Note = model.Note
        };
    }
}
