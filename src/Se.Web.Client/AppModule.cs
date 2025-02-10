using Microsoft.Extensions.DependencyInjection;
using Se.Web.Client.Features;
using Se.Web.Client.Shared;

namespace Se.Web.Client;

public static class AppModule
{
    public static IServiceCollection AddWebClientModule(this IServiceCollection services)
    {
        services.AddScoped<JsonApiClient>();
        services.AddScoped<ISeWebApiClient, SeWebApiClient>();
        
        services.AddScoped<IProductsApiClient, ProductsApiClient>();
        
        return services;
    }
}
