﻿using Microsoft.AspNetCore.Mvc;
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
    public async Task GetAll_ShouldReturnResponse()
    {
        _repo.GetAllAsync()
            .Returns([new ProductEntity(), new ProductEntity()]);
            
        // Act
        var result = (await _sut.GetAll())
            .ToArray();
        
        Assert.Equal(2, result.Length);
    }
    
    [Fact]
    public async Task GetDetails_ShouldReturnOk_WhenEntityExists()
    {
        var id = 1;
        var product = new ProductEntity { Id = id, Name = "Entity" };
        _repo.GetAsync(id).Returns(product);

        var request = new ProductsGetDetailsRequest { Id = id };

        // Act
        var response = await _sut.GetDetails(request) 
            as OkObjectResult;
        
        Assert.NotNull(response);
        Assert.Equal(200, response.StatusCode);
        
        ProductDetails? result = response.Value as ProductDetails;

        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
    }

    [Fact]
    public async Task GetDetails_ShouldReturnNotFound_WhenEntityNotExist()
    {
        var id = 1;
        _repo.GetAsync(id).Returns((ProductEntity?)null);

        var request = new ProductsGetDetailsRequest { Id = id };

        // Act
        NotFoundResult? response = await _sut.GetDetails(request) 
            as NotFoundResult;
        
        Assert.NotNull(response);
        Assert.Equal(404, response.StatusCode);
    }

    [Fact]
    public async Task Create_ShouldReturnOk_WhenModelIsValid()
    {
        var id = 1;
        var request = new ProductsCreateRequest("Name");
        _repo.AddAsync(Arg.Any<ProductEntity>()).Returns(id);

        // Act
        OkObjectResult? response = await _sut.Create(request)
            as OkObjectResult;
        
        Assert.NotNull(response);
        
        int? result = response.Value as int?;

        Assert.NotNull(result);
        Assert.Equal(id, result);
    }

    [Fact]
    public async Task Create_ShouldReturnBadRequest_WhenModelIsInvalid()
    {
        var request = new ProductsCreateRequest(null);
        _sut.ModelState.AddModelError(nameof(request.Name), "Required");

        // Act
        var result = await _sut.Create(request);
        
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Update_ShouldReturnNoContent_WhenEntityExists()
    {
        var id = 1;
        var entity = new ProductEntity { Id = id, Name = "Old Name" };
        _repo.GetAsync(id).Returns(entity);

        var request = new ProductsUpdateRequest("Updated Name") { Id = id };

        // Act
        var result = await _sut.Update(request);
        
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Update_ShouldReturnNotFound_WhenProductDoesNotExist()
    {
        var id = 1;
        _repo.GetAsync(id).Returns((ProductEntity?)null);

        var request = new ProductsUpdateRequest("Updated Name") { Id = id };

        // Act
        var result = await _sut.Update(request);
        
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Update_ShouldReturnBadRequest_WhenModelIsInvalid()
    {
        var request = new ProductsUpdateRequest(null) { Id = 1 };
        _sut.ModelState.AddModelError("Name", "Required");

        // Act
        var result = await _sut.Update(request);
        
        Assert.IsType<BadRequestResult>(result);
    }
    
    [Fact]
    public async Task Delete_ShouldReturnNoContent()
    {
        var request = new ProductsDeleteRequest { Id = 1 };

        // Act
        var result = await _sut.Delete(request);
        
        Assert.IsType<NoContentResult>(result);
    }
}
