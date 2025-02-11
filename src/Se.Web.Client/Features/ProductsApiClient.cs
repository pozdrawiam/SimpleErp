using Se.Contracts.Features.Products;
using Se.Web.Client.Shared;

namespace Se.Web.Client.Features;

public class ProductsApiClient : 
    CrudApiClient<ProductGetDetailsResponse, ProductCreateRequest, ProductUpdateRequest>, 
    IProductsApiClient
{
    public ProductsApiClient(JsonApiClient apiClient) 
        : base(apiClient, "Products")
    {
    }
}
