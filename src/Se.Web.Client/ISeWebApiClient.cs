using Se.Contracts.Features.Products;
using Se.Contracts.Shared.Crud.GetDetails;

namespace Se.Web.Client;

public interface ISeWebApiClient
{
    Task<ProductGetDetailsResponse?> GetDetailsAsync(GetDetailsRequest request, CancellationToken ct = default);
}
