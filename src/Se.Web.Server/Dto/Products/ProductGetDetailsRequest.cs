using Se.Web.Server.Dto.Crud.GetDetails;

namespace Se.Web.Server.Dto.Products;

public record ProductGetDetailsRequest(int Id) : GetDetailsRequest(Id);
