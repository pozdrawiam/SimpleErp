using Microsoft.Extensions.DependencyInjection;
using Se.Application.Features.Products;
using Se.Database.Repositories;

namespace Se.Database;

public static class AppModule
{
    public static IServiceCollection AddDatabaseModule(this IServiceCollection services)
    {
        services.AddSingleton<IProductRepo, ProductMemoryRepo>();
        
        return services;
    }
}
