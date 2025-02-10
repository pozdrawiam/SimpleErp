using Se.Web.Client.Features;

namespace Se.Web.Client;

public interface ISeWebApiClient
{
    public IProductsApiClient Products { get; }
}
