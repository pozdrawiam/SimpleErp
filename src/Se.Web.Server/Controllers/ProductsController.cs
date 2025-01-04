using Microsoft.AspNetCore.Mvc;
using Se.Application.Features.Products;
using Se.Domain.Features.Products;
using Se.Web.Server.Base;
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
    public async Task<IEnumerable<ProductDetails>> GetAll()
    {
        return (await _repo.GetAllAsync())
            .Select(MapToDetails);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ProductDetails), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetails(ProductGetDetailsRequest request)
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
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(ProductCreateRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest();
        
        var product = new ProductEntity
        {
            Id = DateTime.Now.Microsecond,
            Name = request.Name!
        };
        
        int id = await _repo.AddAsync(product);
            
        return Ok(id);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(ProductUpdateRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest();
        
        var product = _repo.GetAsync(request.Id).Result;
        
        if (product is null)
            return NotFound();
        
        product.Name = request.Name!;
        
        await _repo.UpdateAsync(product);
        
        return NoContent();
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMany(ProductDeleteRequest request)
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
