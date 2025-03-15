using MediatR;
using Microsoft.AspNetCore.Mvc;
using Se.Contracts.Features.Orders;
using Se.Contracts.Shared.Crud.Create;
using Se.Contracts.Shared.Crud.DeleteMany;
using Se.Contracts.Shared.Crud.QueryAll;
using Se.Contracts.Shared.Crud.Update;
using Se.Web.Server.Shared;

namespace Se.Web.Server.Controllers;

public class OrdersController : AppApiController
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<QueryAllResponse>> QueryAll(OrdersQueryAllRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        
        var response = await _mediator.Send(request);
        
        return Ok(response);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrderGetDetailsResponse>> GetDetails([FromQuery] OrderGetDetailsRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        
        var response = await _mediator.Send(request);
        
        return Ok(response);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateResponse>> Create(OrderCreateRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        
        int id = await _mediator.Send(request);
            
        return Ok(new CreateResponse(id));
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UpdateResponse>> Update(OrderUpdateRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        
        await _mediator.Send(request);
        
        return Ok(new UpdateResponse());
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DeleteManyResponse>> DeleteMany(OrderDeleteManyRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        
        if (request.Ids?.Count > 0)
            await _mediator.Send(request);
        
        return Ok(new DeleteManyResponse());
    }
}
