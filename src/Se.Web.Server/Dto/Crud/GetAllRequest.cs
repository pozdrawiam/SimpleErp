namespace Se.Web.Server.Dto.Crud;

public record GetAllRequest(string[] Columns, string SortBy, bool SortDesc, int PageSize, int PageNumber, Filter[] Filters);

public record Filter(string Column, FilterOperator Operator, string Value);

public enum FilterOperator
{
    Equals,
    NotEquals,
    GreaterThan,
    GreaterThanOrEqual,
    LessThan,
    LessThanOrEqual,
    Empty,
    NotEmpty
}
