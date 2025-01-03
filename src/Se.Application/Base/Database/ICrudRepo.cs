namespace Se.Application.Base.Database;

public interface ICrudRepo<TEntity>
{
    Task<TEntity?> GetAsync(int id);
    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<int> AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(int entity);
}
