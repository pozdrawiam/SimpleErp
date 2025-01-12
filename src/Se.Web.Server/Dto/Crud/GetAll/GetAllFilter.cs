namespace Se.Web.Server.Dto.Crud.GetAll;

public record GetAllFilter(string Column, GetAllFilterOperator Operator, string Value);