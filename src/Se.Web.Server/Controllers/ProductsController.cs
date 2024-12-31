using Microsoft.AspNetCore.Mvc;
using Se.Web.Server.Dto.Products;

namespace Se.Web.Server.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;

    private static readonly List<ProductDetails> Products = [];
    
    public ProductsController(ILogger<ProductsController> logger)
    {
        _logger = logger;
    }

    #region Read
    
    [HttpGet]
    public IEnumerable<ProductDetails> GetAll()
    {
        return Products.AsEnumerable();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
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
        
        _logger.LogInformation($"Created product: {product.Name}");
            
        return Created();
    }
    
    #endregion
}
