using Se.Application.Base.Database;
using Se.Application.Base.Database.GetAll;

namespace Se.Database.Repositories;

public class CrudRepo<TEntity> : ICrudRepo<TEntity>
{
    public Task<TEntity?> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<GetAllResultDto> GetAllAsync(GetAllArgsDto query)
    {
        throw new NotImplementedException();
    }

    public Task<int> AddAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteManyAsync(IReadOnlyCollection<int> ids)
    {
        throw new NotImplementedException();
    }
}
