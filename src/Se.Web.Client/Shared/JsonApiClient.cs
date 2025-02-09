using System.Net;
using System.Net.Http.Json;

namespace Se.Web.Client.Shared;

public class JsonApiClient
{
    private readonly HttpClient _httpClient;

    public JsonApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<TResult?> GetAsync<TResult>(string url) 
        => await SendAsync<object, TResult?>(HttpMethod.Get, url);
    
    private async Task<TResult?> SendAsync<TData, TResult>(
        HttpMethod method, 
        string url, 
        TData? data = default)
    {
        var request = new HttpRequestMessage(method, $"api/{url}");
        
        if (data != null && (method == HttpMethod.Post || method == HttpMethod.Put))
        {
            request.Content = JsonContent.Create(data);
        }

        HttpResponseMessage response;
        
        try
        {
            response = await _httpClient.SendAsync(request);
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
