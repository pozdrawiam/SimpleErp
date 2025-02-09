using Se.Contracts.Features.Products;
using Se.Contracts.Shared.Crud.GetDetails;
using Se.Web.Client.Shared;

namespace Se.Web.Client;

public class SeWebApiClient : JsonApiClient, ISeWebApiClient
{
    public SeWebApiClient(HttpClient httpClient) 
        : base(httpClient)
    {
    }

    public async Task<ProductGetDetailsResponse?> ProductGetDetailsAsync(GetDetailsRequest request, CancellationToken ct = default) 
        => await GetAsync<ProductGetDetailsResponse>($"Products/GetDetails?Id={request.Id}");
}
