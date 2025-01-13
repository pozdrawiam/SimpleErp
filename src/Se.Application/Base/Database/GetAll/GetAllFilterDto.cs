namespace Se.Application.Base.Database.GetAll;

public record GetAllFilterDto(string Column, GetAllFilterOperatorType Operator, string Value);
