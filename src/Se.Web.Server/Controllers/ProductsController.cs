using Microsoft.AspNetCore.Mvc;
using Se.Application.Features.Products;
using Se.Web.Server.Dto.Products;

namespace Se.Web.Server.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepo _repo;

    public ProductsController(IProductRepo repo)
    {
        _repo = repo;
    }
    
    #region Read
    
    [HttpGet]
    public IEnumerable<ProductDetails> GetAll()
    {
        return _repo.GetAllAsync().Result.Select(MapToDetails);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ProductDetails), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetDetails(ProductsGetDetailsRequest request)
    {
        var product = _repo.GetAsync(request.Id).Result;

        if (product == default)
            return NotFound();

        var details = MapToDetails(product);
        
        return Ok(details);
    }
    
    #endregion

    #region Write
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create(ProductsCreateRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest();
        
        var product = new ProductEntity
        {
            Id = DateTime.Now.Microsecond,
            Name = request.Name!
        };
        
        _repo.AddAsync(product);
            
        return Created();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(ProductsUpdateRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest();
        
        var product = _repo.GetAsync(request.Id).Result;
        
        if (product == default)
            return NotFound();
        
        product.Name = request.Name!;
        
        _repo.UpdateAsync(product);
        
        return NoContent();
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(ProductsDeleteRequest request)
    {
        _repo.DeleteAsync(request.Id);
        
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
