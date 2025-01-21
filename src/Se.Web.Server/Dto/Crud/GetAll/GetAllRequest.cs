namespace Se.Web.Server.Dto.Crud.GetAll;

public record GetAllRequest(
    string[]? Columns = null, 
    string SortBy = "", 
    bool SortDesc = false, 
    int PageSize = 20, 
    int PageNumber = 1, 
    GetAllFilter[]? Filters = null);
