namespace Se.Contracts.Shared.Crud.GetAll;

public record GetAllFilter(string Column, GetAllFilterOperator Operator, string Value);