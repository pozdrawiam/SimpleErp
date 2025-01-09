namespace Se.Web.Server.Dto.Crud;

public record GetAllResponse(IDictionary<string, string[]>[] Data);
