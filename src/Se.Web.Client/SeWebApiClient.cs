using Se.Web.Client.Features;

namespace Se.Web.Client;

public class SeWebApiClient : ISeWebApiClient
{
    public SeWebApiClient(
        IProductsApiClient productsApiClient)
    {
        Products = productsApiClient;
    }
    
    public IProductsApiClient Products { get; }
}
