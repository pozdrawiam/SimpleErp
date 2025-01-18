using Se.Application.Features.Products;
using Se.Domain.Features.Products;
using Se.Web.Server.Base;
using Se.Web.Server.Dto.Products;

namespace Se.Web.Server.Controllers;

public class ProductsController : CrudApiController<ProductEntity, ProductGetDetailsResponse, ProductCreateRequest, ProductUpdateRequest>
{
    public ProductsController(IProductRepo repo) : base(repo)
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
