using System.Net;
using System.Net.Http.Json;
using Se.Contracts.Features.Products;
using Se.Contracts.Shared.Crud.GetDetails;

namespace Se.Web.Client;

public class SeWebApiClient : ISeWebApiClient
{
    private readonly HttpClient _http;

    public SeWebApiClient(HttpClient http)
    {
        _http = http;
    }
    
    public async Task<ProductGetDetailsResponse?> ProductGetDetailsAsync(GetDetailsRequest request, CancellationToken ct = default)
    {
        return await GetAsync<ProductGetDetailsResponse>($"Products/GetDetails?Id={request.Id}");
    }
    
    private async Task<TResult?> GetAsync<TResult>(string url) 
        //where TResult : new() 
        => await SendAsync<object, TResult?>(HttpMethod.Get, url);
    
    private async Task<TResult?> SendAsync<TData, TResult>(
        HttpMethod method, 
        string url, 
        TData? data = default)
        //where TResult : new()
    {
        var request = new HttpRequestMessage(method, $"api/{url}");
        
        if (data != null && (method == HttpMethod.Post || method == HttpMethod.Put))
        {
            request.Content = JsonContent.Create(data);
        }

        HttpResponseMessage response;
        
        try
        {
            response = await _http.SendAsync(request);
        }
        catch (HttpRequestException e) when (e.StatusCode == null)
        {
            return default;
        }

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<TResult>();

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            //AppValidationResult? validationResult = null;
            try
            {
                //validationResult = await response.Content.ReadFromJsonAsync<AppValidationResult>();
            }
            catch
            {
                // ignored
            }

            //if (validationResult != null)
                //throw new AppValidationException(validationResult);
        }
        else if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            //await OnUnauthorized();
        }

        return default;
    }
}
