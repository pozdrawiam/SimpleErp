using Microsoft.Extensions.DependencyInjection;

namespace Se.Web.Client;

public static class AppModule
{
    public static IServiceCollection AddWebClientModule(this IServiceCollection services)
    {
        services.AddScoped<ISeWebApiClient, SeWebApiClient>();
        
        return services;
    }
}
