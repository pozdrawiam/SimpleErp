using Microsoft.AspNetCore.Mvc;
using Se.Application.Base.Database;
using Se.Web.Server.Dto.Products;

namespace Se.Web.Server.Base;

public abstract class CrudApiController< //todo: in progress
    TEntity,
    //TGetAllRequest, TGetAllResponse,
    TGetDetailsRequest, TGetDetailsResponse//,
    //TCreateRequest, TCreateResponse,
    //TUpdateRequest, TUpdateResponse,
    //TDeleteRequest, TDeleteResponse
>
    : AppApiController
    where TGetDetailsRequest : IdRequestBase
{
    private readonly ICrudRepo<TEntity> _repo;

    protected CrudApiController(ICrudRepo<TEntity> repo)
    {
        _repo = repo;
    }

    #region Read

    [HttpGet]
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

    protected abstract TGetDetailsResponse MapEntityToDetailsResponse(TEntity product);
}
