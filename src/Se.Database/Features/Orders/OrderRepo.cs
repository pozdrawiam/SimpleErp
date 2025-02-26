using System.Transactions;
using Dapper.Contrib.Extensions;
using Se.Application.Shared;
using Se.Contracts.Shared.Crud.QueryAll;
using Se.Database.Shared;
using Se.Domain.Features.Orders;

namespace Se.Database.Features.Orders;

public class OrderRepo : Repo, ICrudRepo<OrderEntity>
{
    public OrderRepo(IDbConnectionFactory connectionFactory) 
        : base(connectionFactory)
    {
    }
    
    public Task<OrderEntity?> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<QueryAllResponse> QueryAllAsync(QueryAllRequest query)
    {
        throw new NotImplementedException();
    }

    public async Task<int> AddAsync(OrderEntity entity)
    {
        using var connection = CreateOpenConnection();
        var model = MapEntityToModel(entity);
        var id = await connection.InsertAsync(model);

        return id;
    }

    public Task UpdateAsync(OrderEntity entity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteManyAsync(IReadOnlyCollection<int> ids)
    {
        using var connection = CreateOpenConnection();
        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        foreach (var id in ids)
        {
            var entity = await connection.GetAsync<OrderModel>(id);

            if (entity != null)
            {
                await connection.DeleteAsync(entity);
            }
        }

        transactionScope.Complete();
    }
    
    private OrderModel MapEntityToModel(OrderEntity entity)
    {
        return new OrderModel
        {
            Id = entity.Id,
            CreatedAtUtc = entity.CreatedAtUtc,
            Note = entity.Note
        };
    }
}
