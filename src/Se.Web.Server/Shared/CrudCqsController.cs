using MediatR;
using Microsoft.AspNetCore.Mvc;
using Se.Contracts.Shared.Cqs;
using Se.Contracts.Shared.Crud.Create;
using Se.Contracts.Shared.Crud.DeleteMany;
using Se.Contracts.Shared.Crud.GetDetails;
using Se.Contracts.Shared.Crud.QueryAll;
using Se.Contracts.Shared.Crud.Update;

namespace Se.Web.Server.Shared;

public abstract class CrudCqsController<
    TQueryAllRequest,
    TGetDetailsRequest,
    TGetDetailsResponse,
    TCreateRequest,
    TUpdateRequest,
    TDeleteManyRequest
>
    : AppController
    where TQueryAllRequest : QueryAllRequest
    where TGetDetailsRequest : GetDetailsRequest
    where TGetDetailsResponse : GetDetailsResponseBase
    where TCreateRequest : CreateRequestBase, ICmd
    where TUpdateRequest : UpdateRequestBase
    where TDeleteManyRequest : DeleteManyRequest
{
    private readonly IMediator _mediator;

    protected CrudCqsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    #region Read
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<QueryAllResponse>> QueryAll(TQueryAllRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        
        var response = await _mediator.Send(request);
        
        return Ok(response);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TGetDetailsResponse>> GetDetails([FromQuery] TGetDetailsRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        
        var response = await _mediator.Send(request);
        
        return Ok(response);
    }
    
    #endregion
    
    #region Write
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateResponse>> Create(TCreateRequest request)
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
    public async Task<ActionResult<UpdateResponse>> Update(TUpdateRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        
        await _mediator.Send(request);
        
        return Ok(new UpdateResponse());
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DeleteManyResponse>> DeleteMany(TDeleteManyRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        
        if (request.Ids?.Count > 0)
            await _mediator.Send(request);
        
        return Ok(new DeleteManyResponse());
    }
    
    #endregion
}
