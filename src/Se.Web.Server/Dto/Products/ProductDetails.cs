using Se.Web.Server.Dto.Crud.GetDetails;

namespace Se.Web.Server.Dto.Products;

public record ProductDetails(int Id, string Name) : GetDetailsResponseBase;

