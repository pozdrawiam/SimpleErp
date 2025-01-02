using Microsoft.AspNetCore.Mvc;
using Se.Web.Server.Dto.Products;

namespace Se.Web.Server.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ProductsController : ControllerBase
{
    private static readonly List<ProductDetails> Repo = [];

    #region Read
    
    [HttpGet]
    public IEnumerable<ProductDetails> GetAll()
    {
        return Repo.AsEnumerable();
    }

    [HttpGet]
    [ProducesResponseType(typeof(ProductDetails), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetDetails(ProductsGetDetailsRequest request)
    {
        var details = Repo.FirstOrDefault(p => p.Id == request.Id);
        
        if (details != default)
            return Ok(details);
        
        return NotFound();
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
        
        var product = new ProductDetails(DateTime.Now.Microsecond, request.Name!);
        
        Repo.Add(product);
            
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
        
        var product = Repo.FirstOrDefault(p => p.Id == request.Id);
        
        if (product == default)
            return NotFound();
        
        var product2 = product with { Name = request.Name! };
        int productIndex = Repo.IndexOf(product);
        
        if (productIndex != -1)
        {
            Repo[productIndex] = product2;
        }
        
        return NoContent();
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(ProductsDeleteRequest request)
    {
        var product = Repo.FirstOrDefault(p => p.Id == request.Id);
        
        if (product == default)
            return NotFound();

        Repo.Remove(product);
        
        return NoContent();
    }
    
    #endregion
}
