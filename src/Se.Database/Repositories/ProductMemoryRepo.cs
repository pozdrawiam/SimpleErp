using Se.Application.Features.Products;

namespace Se.Database.Repositories;

public class ProductMemoryRepo : IProductRepo
{
    private static readonly List<ProductEntity> Repo = [];
    
    public Task<ProductEntity?> GetAsync(int id)
    {
        var product = Repo.FirstOrDefault(p => p.Id == id);
        
        return Task.FromResult(product);
    }

    public Task<IEnumerable<ProductEntity>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<ProductEntity>>(Repo);
    }

    public Task<int> AddAsync(ProductEntity entity)
    {
        entity.Id = DateTime.Now.Microsecond;

        Repo.Add(entity);
        
        return Task.FromResult(entity.Id);
    }

    public Task UpdateAsync(ProductEntity entity)
    {
        var existingProduct = Repo.FirstOrDefault(p => p.Id == entity.Id);
        
        if (existingProduct == null)
            throw new InvalidOperationException("Product not found.");

        existingProduct.Name = entity.Name;
        
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var product = Repo.FirstOrDefault(p => p.Id == id);
        
        if (product == null)
            throw new InvalidOperationException("Product not found.");

        Repo.Remove(product);
        
        return Task.CompletedTask;
    }
}

