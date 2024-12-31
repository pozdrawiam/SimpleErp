using Microsoft.AspNetCore.Mvc;
using Se.Web.Server.Dto.Products;

namespace Se.Web.Server.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;

    private static readonly Dictionary<int, string> Products = [];
    
    public ProductsController(ILogger<ProductsController> logger)
    {
        _logger = logger;
    }

    #region Read
    
    [HttpGet]
    public IEnumerable<KeyValuePair<int, string>> GetAll()
    {
        return Products.AsEnumerable();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetDetails(int id)
    {
        if (Products.TryGetValue(id, out var details))
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
        if (ModelState.IsValid)
        {
            Products.Add(DateTime.Now.Microsecond, request.Name!);
            
            _logger.LogInformation($"Created product: {request.Name}");
            
            return Created();
        }
        
        return BadRequest();
    }
    
    #endregion
}
