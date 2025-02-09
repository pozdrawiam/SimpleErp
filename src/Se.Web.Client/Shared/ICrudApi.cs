using Se.Contracts.Shared.Crud.GetDetails;

namespace Se.Web.Client.Shared;

public interface ICrudApiClient<TGetDetailsResponse>
{
    Task<TGetDetailsResponse?> GetDetailsAsync(GetDetailsRequest request, CancellationToken ct = default);
}
