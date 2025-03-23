using System.Transactions;
using Se.Application.Shared;
using Se.Contracts.Shared.Crud.QueryAll;
using Se.Database.Shared;
using Se.Domain.Features.Orders;
using Se.Domain.Shared.ValueObjects;

namespace Se.Database.Features.Orders;

internal class OrderRepo : CrudDomainRepo<OrderModel, OrderEntity>
{
    private readonly ICrudRepo<OrderItemModel> _orderItemRepo;

    public OrderRepo(ICrudRepo<OrderModel> crudRepo, ICrudRepo<OrderItemModel> orderItemRepo) : base(crudRepo)
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
        
        scope.Complete();

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

    protected override async Task<OrderEntity> MapModelToEntity(OrderModel model)
    {
        var entity = new OrderEntity
        {
            Id = model.Id,
            CreatedAtUtc = model.CreatedAtUtc,
            Note = model.Note
        };
        
        var itemModels = await _orderItemRepo.QueryAllAsync(new QueryAllRequest
        {
            Columns = [nameof(OrderItemModel.ProductId), nameof(OrderItemModel.Quantity)],
            Filters = [new QueryAllFilter(nameof(OrderItemModel.OrderId), QueryAllFilterOperator.Equals, model.Id.ToString())],
            PageNumber = 1,
            PageSize = 1000
        });

        foreach (object?[] item in itemModels.Data)
        {
            int productId = (int)item[0]!;
            decimal quantity = (decimal)item[1]!;
            
            entity.AddItem(productId, new Quantity(quantity));
        }

        return entity;
    }
}
