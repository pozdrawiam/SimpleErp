using Microsoft.AspNetCore.Mvc;
using Se.Application.Base.Database;
using Se.Application.Base.Database.GetAll;
using Se.Contracts.Shared.Crud.Create;
using Se.Contracts.Shared.Crud.DeleteMany;
using Se.Contracts.Shared.Crud.GetAll;
using Se.Contracts.Shared.Crud.GetDetails;
using Se.Contracts.Shared.Crud.Update;

namespace Se.Web.Server.Base;

public abstract class CrudApiController<
    TEntity,
    TGetDetailsResponse,
    TCreateRequest,
    TUpdateRequest
>
    : AppApiController
    where TGetDetailsResponse : GetDetailsResponseBase
    where TCreateRequest : CreateRequestBase
    where TUpdateRequest : UpdateRequestBase
{
    private readonly ICrudRepo<TEntity> _repo;

    protected CrudApiController(ICrudRepo<TEntity> repo)
    {
        _repo = repo;
    }

    #region Read

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GetAllResponse>> QueryAll(GetAllRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        
        var filters = request.Filters?
            .Select(x => new GetAllFilterDto(x.Column, (GetAllFilterOperatorType)x.Operator, x.Value))
            .ToArray()
            ?? [];
        
        var argsDto = new GetAllArgsDto(request.Columns ?? [], request.SortBy, request.SortDesc, 
            request.PageSize, request.PageNumber, filters);
        
        var result = await _repo.GetAllAsync(argsDto);
        
        var response = new GetAllResponse(result.Data, result.TotalCount);
        
        return Ok(response);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetDetailsResponseBase>> GetDetails([FromQuery] GetDetailsRequest request)
    {
        TEntity? entity = await _repo.GetAsync(request.Id);

        if (entity == null)
            return NotFound();

        var response = MapEntityToGetDetailsResponse(entity);

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
        
        var entity = MapCreateRequestToEntity(request);
        int id = await _repo.AddAsync(entity);
            
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
        
        var entity = await _repo.GetAsync(request.Id);
        
        if (entity is null)
            return NotFound();
        
        UpdateEntityByUpdateRequest(entity, request);
        await _repo.UpdateAsync(entity);
        
        return Ok(new UpdateResponse());
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DeleteManyResponse>> DeleteMany(DeleteManyRequest request)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
        
        if (request.Ids?.Count > 0)
            await _repo.DeleteManyAsync(request.Ids);
        
        return Ok(new DeleteManyResponse());
    }
    
    #endregion

    protected abstract TGetDetailsResponse MapEntityToGetDetailsResponse(TEntity product);
    
    protected abstract TEntity MapCreateRequestToEntity(TCreateRequest request);
    
    protected abstract void UpdateEntityByUpdateRequest(TEntity entity, TUpdateRequest request);
}
