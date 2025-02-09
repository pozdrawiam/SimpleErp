using Se.Web.Client.Features;

namespace Se.Web.Client;

public class SeWebApiClient2 : ISeWebApiClient2
{
    public SeWebApiClient2(
        IProductsApiClient productsApiClient)
    {
        Products = productsApiClient;
    }
    
    public IProductsApiClient Products { get; }
}
