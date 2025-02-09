using Se.Application.Shared;
using Se.Contracts.Features.Products;
using Se.Domain.Features.Products;
using Se.Web.Server.Shared;

namespace Se.Web.Server.Controllers;

public class ProductsController : CrudApiController<ProductEntity, ProductGetDetailsResponse, ProductCreateRequest, ProductUpdateRequest>
{
    public ProductsController(ICrudRepo<ProductEntity> repo) : base(repo)
    {
    }

    protected override ProductGetDetailsResponse MapEntityToGetDetailsResponse(ProductEntity product) => 
        new(product.Id, product.Name);

    protected override ProductEntity MapCreateRequestToEntity(ProductCreateRequest request) => 
        new() { Name = request.Name! };

    protected override void UpdateEntityByUpdateRequest(ProductEntity entity, ProductUpdateRequest request)
    {
        entity.Name = request.Name!;
    }
}
