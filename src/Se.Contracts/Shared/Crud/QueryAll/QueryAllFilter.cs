namespace Se.Contracts.Shared.Crud.QueryAll;

public record QueryAllFilter(string Column, QueryAllFilterOperator Operator, string Value);
