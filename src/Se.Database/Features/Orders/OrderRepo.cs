using Se.Database.Shared;
using Se.Domain.Features.Orders;

namespace Se.Database.Features.Orders;

public class OrderRepo : CrudDomainRepo<OrderModel, OrderEntity>
{
    public OrderRepo(CrudRepo<OrderModel> crudRepo) : base(crudRepo)
    {
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
