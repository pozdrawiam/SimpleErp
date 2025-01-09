using Microsoft.AspNetCore.Mvc;
using Se.Application.Base.Database;
using Se.Web.Server.Dto.Crud;

namespace Se.Web.Server.Base;

public abstract class CrudApiController< //todo: in progress
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

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GetAllResponse>> GetAll(GetAllRequest request)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetDetailsResponseBase>> GetDetails(GetDetailsRequest request)
    {
        TEntity? entity = await _repo.GetAsync(request.Id);

        if (entity == null)
            return NotFound();

        var response = MapEntityToDetailsResponse(entity);

        return Ok(response);
    }

    #endregion
    
    #region Write

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateResponse>> Create(TCreateRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UpdateResponse>> Update(TUpdateRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeleteManyResponse>> DeleteMany(DeleteManyRequest request)
    {
        throw new NotImplementedException();
    }
    
    #endregion

    protected abstract TGetDetailsResponse MapEntityToDetailsResponse(TEntity product);
}
