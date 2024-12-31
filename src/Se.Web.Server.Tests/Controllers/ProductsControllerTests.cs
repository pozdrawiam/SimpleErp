using Microsoft.Extensions.Logging;
using NSubstitute;
using Se.Web.Server.Controllers;

namespace Se.Web.Server.Tests.Controllers;

public class ProductsControllerTests
{
    private readonly ProductsController _sut;
    
    private readonly ILogger<ProductsController> _loggerMock = Substitute.For<ILogger<ProductsController>>();

    public ProductsControllerTests()
    {
        _sut = new ProductsController(_loggerMock);
    }

    [Fact]
    public void GetAll_ShouldReturnResponse()
    {
        // Act
        var result = _sut.GetAll();
        
        Assert.Empty(result);
    }
}
