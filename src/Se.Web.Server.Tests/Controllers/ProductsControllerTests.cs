using Se.Web.Server.Controllers;

namespace Se.Web.Server.Tests.Controllers;

public class ProductsControllerTests
{
    private readonly ProductsController _sut = new();
    
    [Fact]
    public void GetAll_ShouldReturnResponse()
    {
        // Act
        var result = _sut.GetAll();
        
        Assert.Empty(result);
    }
}
