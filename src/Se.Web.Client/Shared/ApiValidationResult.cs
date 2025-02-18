namespace Se.Web.Client.Shared;

public class ApiValidationResult
{
    public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
}
