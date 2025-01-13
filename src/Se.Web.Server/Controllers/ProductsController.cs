using Microsoft.AspNetCore.Mvc;
using Se.Application.Base.Database.GetAll;
using Se.Application.Features.Products;
using Se.Domain.Features.Products;
using Se.Web.Server.Base;
using Se.Web.Server.Dto.Crud.Create;
using Se.Web.Server.Dto.Crud.DeleteMany;
using Se.Web.Server.Dto.Crud.GetAll;
using Se.Web.Server.Dto.Crud.GetDetails;
using Se.Web.Server.Dto.Crud.Update;
using Se.Web.Server.Dto.Products;

namespace Se.Web.Server.Controllers;

public class ProductsController : AppApiController
{
    private readonly IProductRepo _repo;

    public ProductsController(IProductRepo repo)
    {
        _repo = repo;
    }
    
    #region Read
    
    [HttpGet]
    public async Task<GetAllResponse> GetAll(GetAllRequest request) //todo 
    {
        var dto = new GetAllArgsDto(request.Columns, request.SortBy, request.SortDesc, request.PageSize, request.PageNumber, new GetAllFilterDto[0]);
        var result = await _repo.GetAllAsync(dto);
        var response = new GetAllResponse(result.Data);
        
        return response;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ProductDetails), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetails(GetDetailsRequest request)
    {
        var product = await _repo.GetAsync(request.Id);

        if (product is null)
            return NotFound();

        var details = MapToDetails(product);
        
        return Ok(details);
    }
    
    #endregion

    #region Write
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateResponse>> Create(ProductCreateRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest();
        
        var product = new ProductEntity
        {
            Id = DateTime.Now.Microsecond,
            Name = request.Name!
        };
        
        int id = await _repo.AddAsync(product);
            
        return Ok(new CreateResponse(id));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UpdateResponse>> Update(ProductUpdateRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest();
        
        var product = _repo.GetAsync(request.Id).Result;
        
        if (product is null)
            return NotFound();
        
        product.Name = request.Name!;
        
        await _repo.UpdateAsync(product);
        
        return Ok(new UpdateResponse());
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMany(DeleteManyRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        
        await _repo.DeleteManyAsync(request.Ids!);
        
        return NoContent();
    }
    
    #endregion

    #region Shared

    private ProductDetails MapToDetails(ProductEntity entity)
    {
        return new ProductDetails(entity.Id, entity.Name);
    }

    #endregion
}
