namespace Se.Application.Base.Database.GetAll;

public record GetAllArgsDto(string[] Columns, string SortBy, bool SortDesc, int PageSize, int PageNumber, GetAllFilterDto[] Filters);
