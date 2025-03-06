using MediatR;
using Microsoft.AspNetCore.Mvc;
using Se.Contracts.Features.Orders;
using Se.Contracts.Shared.Crud.Create;
using Se.Contracts.Shared.Crud.QueryAll;
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
}
