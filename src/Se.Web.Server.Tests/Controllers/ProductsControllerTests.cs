﻿using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Se.Application.Features.Products;
using Se.Domain.Features.Products;
using Se.Web.Server.Controllers;
using Se.Web.Server.Dto.Crud;
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
        const int id = 1;
        var product = new ProductEntity { Id = id, Name = "Entity" };
        _repo.GetAsync(id).Returns(product);

        var request = new GetDetailsRequest(id);

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
        const int id = 1;
        _repo.GetAsync(id).Returns((ProductEntity?)null);

        var request = new GetDetailsRequest(id);

        // Act
        NotFoundResult? response = await _sut.GetDetails(request) 
            as NotFoundResult;
        
        Assert.NotNull(response);
        Assert.Equal(404, response.StatusCode);
    }

    [Fact]
    public async Task Create_ShouldReturnOk_WhenModelIsValid()
    {
        const int id = 1;
        var request = new ProductCreateRequest("Name");
        _repo.AddAsync(Arg.Any<ProductEntity>()).Returns(id);

        // Act
        var result = (await _sut.Create(request))
            .Result as OkObjectResult;
        
        Assert.NotNull(result);
        
        var response = result.Value as CreateResponse;
        
        Assert.NotNull(response);
        Assert.Equal(id, response.Id);
    }

    [Fact]
    public async Task Create_ShouldReturnBadRequest_WhenModelIsInvalid()
    {
        var request = new ProductCreateRequest(null);
        _sut.ModelState.AddModelError(nameof(request.Name), "Required");

        // Act
        var actionResult = await _sut.Create(request);
        
        Assert.IsType<BadRequestResult>(actionResult.Result);
    }

    [Fact]
    public async Task Update_ShouldReturnOk_WhenEntityExists()
    {
        const int id = 1;
        var entity = new ProductEntity { Id = id, Name = "Old Name" };
        _repo.GetAsync(id).Returns(entity);

        var request = new ProductUpdateRequest("Updated Name") { Id = id };

        // Act
        var actionResult = (await _sut.Update(request))
            .Result as OkObjectResult;
        
        Assert.NotNull(actionResult);
        
        var response = actionResult.Value as UpdateResponse;
        
        Assert.NotNull(response);
    }

    [Fact]
    public async Task Update_ShouldReturnNotFound_WhenProductDoesNotExist()
    {
        const int id = 1;
        _repo.GetAsync(id).Returns((ProductEntity?)null);

        var request = new ProductUpdateRequest("Updated Name") { Id = id };

        // Act
        var actionResult = await _sut.Update(request);
        
        Assert.IsType<NotFoundResult>(actionResult.Result);
    }

    [Fact]
    public async Task Update_ShouldReturnBadRequest_WhenModelIsInvalid()
    {
        var request = new ProductUpdateRequest(null) { Id = 1 };
        _sut.ModelState.AddModelError("Name", "Required");

        // Act
        var actionResult = await _sut.Update(request);
        
        Assert.IsType<BadRequestResult>(actionResult.Result);
    }
    
    [Fact]
    public async Task DeleteMany_ShouldReturnNoContent()
    {
        var request = new ProductDeleteRequest { Ids = [ 1, 2 ] };

        // Act
        var result = await _sut.DeleteMany(request);
        
        Assert.IsType<NoContentResult>(result);
    }
}
