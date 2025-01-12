using Se.Application.Base.Database.GetAll;
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

    public Task<GetAllResult> GetAllAsync(GetAllDto query)
    {
        IEnumerable<ProductEntity> filteredRepo = Repo;

        foreach (var filter in query.Filters)
        {
            filteredRepo = filter.Operator switch
            {
                GetAllFilterOperator.Equals => filteredRepo.Where(p =>
                    GetPropertyValue(p, filter.Column)?.ToString() == filter.Value),

                GetAllFilterOperator.NotEquals => filteredRepo.Where(p =>
                    GetPropertyValue(p, filter.Column)?.ToString() != filter.Value),

                GetAllFilterOperator.GreaterThan => filteredRepo.Where(p =>
                    double.TryParse(GetPropertyValue(p, filter.Column)?.ToString(), out var val) && val > double.Parse(filter.Value)),

                GetAllFilterOperator.GreaterThanOrEqual => filteredRepo.Where(p =>
                    double.TryParse(GetPropertyValue(p, filter.Column)?.ToString(), out var val) && val >= double.Parse(filter.Value)),

                GetAllFilterOperator.LessThan => filteredRepo.Where(p =>
                    double.TryParse(GetPropertyValue(p, filter.Column)?.ToString(), out var val) && val < double.Parse(filter.Value)),

                GetAllFilterOperator.LessThanOrEqual => filteredRepo.Where(p =>
                    double.TryParse(GetPropertyValue(p, filter.Column)?.ToString(), out var val) && val <= double.Parse(filter.Value)),

                GetAllFilterOperator.Empty => filteredRepo.Where(p =>
                    string.IsNullOrEmpty(GetPropertyValue(p, filter.Column)?.ToString())),

                GetAllFilterOperator.NotEmpty => filteredRepo.Where(p =>
                    !string.IsNullOrEmpty(GetPropertyValue(p, filter.Column)?.ToString())),

                _ => filteredRepo
            };
        }

        // Zastosuj sortowanie
        if (!string.IsNullOrEmpty(query.SortBy))
        {
            filteredRepo = query.SortDesc
                ? filteredRepo.OrderByDescending(p => GetPropertyValue(p, query.SortBy))
                : filteredRepo.OrderBy(p => GetPropertyValue(p, query.SortBy));
        }

        // Zastosuj paginację
        var paginatedRepo = filteredRepo
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToList();

        // Przygotuj wynik
        var resultData = paginatedRepo
            .Select(p => query.Columns.ToDictionary(
                column => column,
                column => new[] { GetPropertyValue(p, column)?.ToString() ?? string.Empty }
            ))
            .ToArray();

        return Task.FromResult(new GetAllResult(resultData));

        // Zastosuj filtrowanie
        object? GetPropertyValue(object obj, string propertyName)
        {
            var property = obj.GetType().GetProperty(propertyName);
            return property?.GetValue(obj);
        }
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

    public Task DeleteManyAsync(IReadOnlyCollection<int> ids)
    {
        var entities = Repo.Where(x => ids.Contains(x.Id));

        foreach (var entity in entities)
        {
            Repo.Remove(entity);
        }

        return Task.CompletedTask;
    }
}
