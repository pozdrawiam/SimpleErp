using Se.Contracts.Shared.Crud.Create;
using Se.Contracts.Shared.Crud.DeleteMany;
using Se.Contracts.Shared.Crud.GetDetails;
using Se.Contracts.Shared.Crud.QueryAll;
using Se.Contracts.Shared.Crud.Update;

namespace Se.Web.Client.Shared;

public class CrudApiClient<TGetDetailsResponse, TCreateRequest, TUpdateRequest> 
    : ICrudApiClient<TGetDetailsResponse, TCreateRequest, TUpdateRequest>
    where TGetDetailsResponse : GetDetailsResponseBase
    where TCreateRequest : CreateRequestBase
    where TUpdateRequest : UpdateRequestBase
{
    private readonly JsonApiClient _apiClient;
    private readonly string _resourceName;

    protected CrudApiClient(JsonApiClient apiClient, string resourceName)
    {
        _apiClient = apiClient;
        _resourceName = resourceName;
    }

    public Task<QueryAllResponse?> QueryAllAsync(QueryAllRequest request, CancellationToken ct = default)
        => _apiClient.PostAsync<QueryAllRequest, QueryAllResponse>($"{_resourceName}/QueryAll", request);

    public Task<TGetDetailsResponse?> GetDetailsAsync(GetDetailsRequest request, CancellationToken ct = default)
        => _apiClient.GetAsync<TGetDetailsResponse>($"{_resourceName}/GetDetails?Id={request.Id}");

    public Task<CreateResponse?> CreateAsync(TCreateRequest request, CancellationToken ct = default)
        => _apiClient.PostAsync<TCreateRequest, CreateResponse>($"{_resourceName}/Create", request);

    public Task<UpdateResponse?> UpdateAsync(TUpdateRequest request, CancellationToken ct = default)
        => _apiClient.PutAsync<TUpdateRequest, UpdateResponse>($"{_resourceName}/Update", request);

    public Task<DeleteManyResponse?> DeleteManyAsync(DeleteManyRequest request, CancellationToken ct = default)
        => _apiClient.DeleteAsync<DeleteManyRequest, DeleteManyResponse>($"{_resourceName}/DeleteMany", request);
}
