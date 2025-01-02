using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Se.Application.Features.Products;
using Se.Web.Server.Controllers;
using Se.Web.Server.Dto.Products;

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
    
    [Fact]
    public void GetDetails_ShouldReturnOk_WhenEntityExists()
    {
        var id = 1;
        var product = new ProductEntity { Id = id, Name = "Entity" };
        _repo.GetAsync(id).Returns(product);

        var request = new ProductsGetDetailsRequest { Id = id };

        // Act
        var response = _sut.GetDetails(request) as OkObjectResult;
        
        Assert.NotNull(response);
        Assert.Equal(200, response.StatusCode);
        
        ProductDetails? result = response.Value as ProductDetails;

        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
    }

    [Fact]
    public void GetDetails_ShouldReturnNotFound_WhenEntityNotExist()
    {
        int id = 1;
        _repo.GetAsync(id).Returns((ProductEntity?)null);

        var request = new ProductsGetDetailsRequest { Id = id };

        // Act
        NotFoundResult? response = _sut.GetDetails(request) as NotFoundResult;
        
        Assert.NotNull(response);
        Assert.Equal(404, response.StatusCode);
    }

}
