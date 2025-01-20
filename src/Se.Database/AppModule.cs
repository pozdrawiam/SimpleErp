using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Se.Application.Features.Products;
using Se.Database.DbConnection;
using Se.Database.Repositories;

namespace Se.Database;

public static class AppModule
{
    public static IServiceCollection AddDatabaseModule(this IServiceCollection services, IConfiguration configuration)
    {
        string dbConnectionString = configuration["DbConnection"] ?? 
                                    throw new InvalidOperationException("DbConnection key is missing in the configuration.");
        
        services.AddSingleton<IDbConnectionFactory>(new SqlServerConnectionFactory(dbConnectionString));
        
        services.AddTransient<IProductRepo, ProductSqlRepo>();
        
        return services;
    }
}
