using System.ComponentModel;

namespace Se.Contracts.Shared.Crud.QueryAll;

public class QueryAllRequest
{
    [DefaultValue(null)]
    public string[]? Columns { get; init; }
    
    [DefaultValue("")]
    public string SortBy { get; init; } = "";
    
    [DefaultValue(false)]
    public bool SortDesc { get; init; }
    
    [DefaultValue(20)]
    public int PageSize { get; init; } = 20;
    
    [DefaultValue(1)]
    public int PageNumber { get; init; } = 1;
    
    [DefaultValue(null)]
    public QueryAllFilter[]? Filters { get; init; }
}
