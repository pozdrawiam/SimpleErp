using Se.Contracts.Shared.Crud.QueryAll;

namespace Se.Application.Base.Database;

public interface ICrudRepo<TEntity>
{
    Task<TEntity?> GetAsync(int id);
    Task<QueryAllResponse> GetAllAsync(QueryAllRequest query);

    Task<int> AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteManyAsync(IReadOnlyCollection<int> ids);
}
