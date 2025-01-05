using Microsoft.AspNetCore.Mvc;
using Se.Application.Base.Database;
using Se.Web.Server.Dto.Crud;
using Se.Web.Server.Dto.Products;

namespace Se.Web.Server.Base;

public abstract class CrudApiController< //todo: in progress
    TEntity,
    TGetAllRequest, TGetAllResponse,
    TGetDetailsRequest, TGetDetailsResponse,
    TCreateRequest, TCreateResponse,
    TUpdateRequest, TUpdateResponse,
    TDeleteManyRequest, TDeleteManyResponse
>
    : AppApiController
    where TGetDetailsRequest : GetDetailsRequest
    where TGetDetailsResponse : GetDetailsResponse
    where TCreateRequest : CreateRequest
    where TCreateResponse : CreateResponse
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
    public async Task<ActionResult<TGetAllResponse>> GetAll(TGetAllRequest request)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TGetDetailsResponse>> GetDetails(TGetDetailsRequest request)
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
    public async Task<ActionResult<TCreateResponse>> Create(TCreateRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TUpdateResponse>> Update(TUpdateRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TDeleteManyResponse>> DeleteMany(TDeleteManyRequest request)
    {
        throw new NotImplementedException();
    }
    
    #endregion

    protected abstract TGetDetailsResponse MapEntityToDetailsResponse(TEntity product);
}
