using Microsoft.AspNetCore.Mvc;
using Se.Web.Server.Dto.Products;

namespace Se.Web.Server.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;

    private static readonly List<string> Products = [];
    
    public ProductsController(ILogger<ProductsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<string> GetAll()
    {
        return Products;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create(ProductsCreateRequest request)
    {
        if (ModelState.IsValid)
        {
            Products.Add(request.Name!);
            
            _logger.LogInformation($"Created product: {request.Name}");
            
            return Created();
        }
        
        return BadRequest();
    }
}
