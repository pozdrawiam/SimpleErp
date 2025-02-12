using Se.Contracts.Shared.Crud.Create;
using Se.Contracts.Shared.Crud.DeleteMany;
using Se.Contracts.Shared.Crud.GetDetails;
using Se.Contracts.Shared.Crud.QueryAll;
using Se.Contracts.Shared.Crud.Update;

namespace Se.Web.Client.Shared;

public interface ICrudApiClient<TGetDetailsResponse, TCreateRequest, TUpdateRequest>
    where TGetDetailsResponse : GetDetailsResponseBase
    where TCreateRequest : CreateRequestBase
    where TUpdateRequest : UpdateRequestBase
{
    Task<QueryAllResponse?> QueryAllAsync(QueryAllRequest request, CancellationToken ct = default);
    Task<TGetDetailsResponse?> GetDetailsAsync(GetDetailsRequest request, CancellationToken ct = default);

    Task<CreateResponse?> CreateAsync(TCreateRequest request, CancellationToken ct = default);
    Task<UpdateResponse?> UpdateAsync(TUpdateRequest request, CancellationToken ct = default);
    Task<DeleteManyResponse?> DeleteManyAsync(DeleteManyRequest request, CancellationToken ct = default);
}
