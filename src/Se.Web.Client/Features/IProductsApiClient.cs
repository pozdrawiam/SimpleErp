using Se.Contracts.Features.Products;
using Se.Web.Client.Shared;

namespace Se.Web.Client.Features;

public interface IProductsApiClient : ICrudApiClient<ProductGetDetailsResponse, ProductCreateRequest, ProductUpdateRequest>
{
}
