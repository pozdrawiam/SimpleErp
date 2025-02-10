using Se.Contracts.Shared.Crud.GetDetails;
using Se.Contracts.Shared.Crud.QueryAll;

namespace Se.Web.Client.Shared;

public class CrudApiClient<TGetDetailsResponse> : ICrudApiClient<TGetDetailsResponse>
{
    private readonly JsonApiClient _apiClient;
    private readonly string _resourceName;

    public CrudApiClient(JsonApiClient apiClient, string resourceName)
    {
        _apiClient = apiClient;
        _resourceName = resourceName;
    }

    public Task<QueryAllResponse> QueryAllAsync(QueryAllRequest request, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<TGetDetailsResponse?> GetDetailsAsync(GetDetailsRequest request, CancellationToken ct = default)
        => await _apiClient.GetAsync<TGetDetailsResponse>($"{_resourceName}/GetDetails?Id={request.Id}");
}
