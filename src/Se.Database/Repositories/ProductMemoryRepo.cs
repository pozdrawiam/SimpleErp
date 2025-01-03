using Se.Application.Features.Products;
using Se.Domain.Features.Products;

namespace Se.Database.Repositories;

public class ProductMemoryRepo : IProductRepo
{
    private static readonly List<ProductEntity> Repo =
    [
        new() { Id = 1, Name = "Apple" },
        new() { Id = 2, Name = "Bananas" },
        new() { Id = 3, Name = "Orange" },
        new() { Id = 4, Name = "Pineapple" },
        new() { Id = 5, Name = "Pineapple 2" },
        new() { Id = 6, Name = "Orange 2" },
        new() { Id = 7, Name = "Orange 3" },
        new() { Id = 8, Name = "Orange 4" },
        new() { Id = 9, Name = "Orange 5" },
        new() { Id = 10, Name = "Orange 6" },
        new() { Id = 11, Name = "Orange 7" },
        new() { Id = 12, Name = "Orange 8" },
        new() { Id = 13, Name = "Orange 9" },
        new() { Id = 14, Name = "Orange 10" }
    ];

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
            throw new KeyNotFoundException("Entity not found.");

        Repo.Remove(product);

        return Task.CompletedTask;
    }
}
