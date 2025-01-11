namespace Se.Application.Base.Database.GetAll;

public record GetAllFilter(string Column, GetAllFilterOperator Operator, string Value);