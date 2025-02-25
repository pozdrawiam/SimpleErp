using Se.Application.Shared;
using Se.Contracts.Shared.Crud.QueryAll;
using Se.Domain.Features.Orders;

namespace Se.Database.Repositories;

public class OrderRepo : ICrudRepo<OrderEntity>
{
    public Task<OrderEntity?> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<QueryAllResponse> QueryAllAsync(QueryAllRequest query)
    {
        throw new NotImplementedException();
    }

    public Task<int> AddAsync(OrderEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(OrderEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteManyAsync(IReadOnlyCollection<int> ids)
    {
        throw new NotImplementedException();
    }
}
