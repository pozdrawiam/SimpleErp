namespace Se.Web.Server.Dto.Crud.GetAll;

public record GetAllRequest(string[] Columns, string SortBy, bool SortDesc, int PageSize, int PageNumber, GetAllFilter[] Filters);
