using Se.Web.Server.Dto.Crud.GetDetails;

namespace Se.Web.Server.Dto.Products;

public record ProductGetDetailsResponse(int Id, string Name) : GetDetailsResponseBase;

