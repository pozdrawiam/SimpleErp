using NSubstitute;
using Se.Application.Features.Products;
using Se.Web.Server.Controllers;

namespace Se.Web.Server.Tests.Controllers;

public class ProductsControllerTests
{
    private readonly ProductsController _sut;
    
    private readonly IProductRepo _repo = Substitute.For<IProductRepo>();

    public ProductsControllerTests()
    {
        _sut = new ProductsController(_repo);
    }
    
    [Fact]
    public void GetAll_ShouldReturnResponse()
    {
        _repo.GetAllAsync()
            .Returns([new ProductEntity(), new ProductEntity()]);
            
        // Act
        var result = _sut.GetAll().ToArray();
        
        Assert.Equal(2, result.Length);
    }
}
