using Se.Application.Base.Database.GetAll;

namespace Se.Application.Base.Database;

public interface ICrudRepo<TEntity>
{
    Task<TEntity?> GetAsync(int id);
    Task<GetAllResultDto> GetAllAsync(GetAllDto query);

    Task<int> AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteManyAsync(IReadOnlyCollection<int> ids);
}
