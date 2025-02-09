using Se.Contracts.Shared.Crud.GetDetails;

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
    
    public async Task<TGetDetailsResponse?> GetDetailsAsync(GetDetailsRequest request, CancellationToken ct = default)
        => await _apiClient.GetAsync<TGetDetailsResponse>($"{_resourceName}/GetDetails?Id={request.Id}");
}
