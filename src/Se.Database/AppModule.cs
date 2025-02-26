using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Se.Application.Shared;
using Se.Database.Features.Orders;
using Se.Database.Shared;
using Se.Domain.Features.Orders;
using Se.Domain.Features.Products;

namespace Se.Database;

public static class AppModule
{
    public static IServiceCollection AddDatabaseModule(this IServiceCollection services, IConfiguration configuration)
    {
        string dbConnectionString = configuration["DbConnection"] ?? 
                                    throw new InvalidOperationException("DbConnection key is missing in the configuration.");
        
        services.AddSingleton<IDbConnectionFactory>(new SqlServerConnectionFactory(dbConnectionString));
        
        services.AddTransient<ICrudRepo<OrderEntity>, OrderRepo>();
        services.AddTransient<ICrudRepo<ProductEntity>, CrudRepo<ProductEntity>>();
        
        return services;
    }
}
