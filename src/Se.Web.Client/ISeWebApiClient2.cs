using Se.Contracts.Features.Products;
using Se.Contracts.Shared.Crud.GetDetails;
using Se.Web.Client.Features;

namespace Se.Web.Client;

public interface ISeWebApiClient2
{
    public IProductsApiClient Products { get; }
}
