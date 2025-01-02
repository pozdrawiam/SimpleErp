namespace Se.Application.Base.Database;

public interface ICrudRepo<TEntity, TId>
{
    Task<TEntity?> GetAsync(TId id);
    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<TId> AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TId entity);
}
