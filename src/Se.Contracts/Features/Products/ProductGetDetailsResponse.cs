using Se.Contracts.Shared.Crud.GetDetails;

namespace Se.Contracts.Features.Products;

public record ProductGetDetailsResponse(int Id, string Name) : GetDetailsResponseBase;

