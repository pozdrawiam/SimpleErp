using Se.Application.Shared;
using Se.Contracts.Shared.Crud.QueryAll;

namespace Se.Database.Shared;

public abstract class CrudDomainRepo<TDbModel, TDomainEntity> : ICrudRepo<TDomainEntity> 
    where TDbModel : class
    where TDomainEntity : class
{
    private readonly CrudRepo<TDbModel> _crudRepo;

    protected CrudDomainRepo(CrudRepo<TDbModel> crudRepo)
    {
        _crudRepo = crudRepo;
    }

    public async Task<TDomainEntity?> GetAsync(int id)
    {
        var model = await _crudRepo.GetAsync(id);
        
        if (model == null)
            return null;
        
        return MapModelToEntity(model);
    }

    public Task<QueryAllResponse> QueryAllAsync(QueryAllRequest query)
    {
        return _crudRepo.QueryAllAsync(query);
    }

    public async Task<int> AddAsync(TDomainEntity entity)
    {
        var model = MapEntityToModel(entity);
        
        return await _crudRepo.AddAsync(model);
    }

    public Task UpdateAsync(TDomainEntity entity)
    {
        var model = MapEntityToModel(entity);
        
        return _crudRepo.UpdateAsync(model);
    }

    public Task DeleteManyAsync(IReadOnlyCollection<int> ids)
    {
        return _crudRepo.DeleteManyAsync(ids);
    }
    
    protected abstract TDbModel MapEntityToModel(TDomainEntity entity);
    
    protected abstract TDomainEntity MapModelToEntity(TDbModel model);
}
