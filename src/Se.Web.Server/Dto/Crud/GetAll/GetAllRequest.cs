using System.ComponentModel;

namespace Se.Web.Server.Dto.Crud.GetAll;

public class GetAllRequest
{
    [DefaultValue(null)]
    public string[]? Columns { get; init; } = null;
    
    [DefaultValue("")]
    public string SortBy { get; init; } = "";
    
    [DefaultValue(false)]
    public bool SortDesc { get; init; } = false;
    
    [DefaultValue(20)]
    public int PageSize { get; init; } = 20;
    
    [DefaultValue(1)]
    public int PageNumber { get; init; } = 1;
    
    [DefaultValue(null)]
    public GetAllFilter[]? Filters { get; init; } = null;
}
