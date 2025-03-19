using Se.Contracts.Shared.Crud.Create;
using Se.Contracts.Shared.Crud.DeleteMany;
using Se.Contracts.Shared.Crud.GetDetails;
using Se.Contracts.Shared.Crud.QueryAll;
using Se.Contracts.Shared.Crud.Update;

namespace Se.Web.Server.Shared;

public abstract class CrudApiController2<
    TQueryAllRequest,
    TGetDetailsRequest,
    TGetDetailsResponse,
    TCreateRequest,
    TUpdateRequest,
    TDeleteManyRequest
>
    : AppApiController
    where TQueryAllRequest : QueryAllRequest
    where TGetDetailsRequest : GetDetailsRequest
    where TGetDetailsResponse : GetDetailsResponseBase
    where TCreateRequest : CreateRequestBase
    where TUpdateRequest : UpdateRequestBase
    where TDeleteManyRequest : DeleteManyRequest
{
}
