using Se.Contracts.Shared.Crud.GetDetails;
using Se.Contracts.Shared.Crud.QueryAll;

namespace Se.Web.Client.Shared;

public interface ICrudApiClient<TGetDetailsResponse>
{
    Task<QueryAllResponse> QueryAllAsync(QueryAllRequest request, CancellationToken ct = default);
    Task<TGetDetailsResponse?> GetDetailsAsync(GetDetailsRequest request, CancellationToken ct = default);
}
