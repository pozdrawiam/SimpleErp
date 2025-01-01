using Microsoft.AspNetCore.Mvc;
using Se.Web.Server.Dto.Products;

namespace Se.Web.Server.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ProductsController : ControllerBase
{
    private static readonly List<ProductDetails> Products = [];

    #region Read
    
    [HttpGet]
    public IEnumerable<ProductDetails> GetAll()
    {
        return Products.AsEnumerable();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductDetails), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetDetails(int id)
    {
        var details = Products.FirstOrDefault(p => p.Id == id);
        
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
        
        Products.Add(product);
            
        return Created();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(int id, ProductsUpdateRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest();
        
        var product = Products.FirstOrDefault(p => p.Id == id);
        
        if (product == default)
            return NotFound();
        
        var product2 = product with { Name = request.Name! };
        int productIndex = Products.IndexOf(product);
        
        if (productIndex != -1)
        {
            Products[productIndex] = product2;
        }
        
        return NoContent();
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);
        
        if (product == default)
            return NotFound();

        Products.Remove(product);
        
        return NoContent();
    }
    
    #endregion
}
