namespace Se.Web.Client.Shared;

public class ApiValidationException : Exception
{
    public ApiValidationException(ApiValidationResult validationResult)
    {
        Errors = validationResult.Errors;
    }
    
    public IDictionary<string, string[]> Errors { get; }
}
