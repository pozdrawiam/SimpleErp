namespace Se.Application.Base.Database.GetAll;

public record GetAllFilterDto(string Column, GetAllFilterOperator Operator, string Value);
