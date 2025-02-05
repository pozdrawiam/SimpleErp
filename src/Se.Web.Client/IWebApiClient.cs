﻿using Se.Contracts.Features.Products;
using Se.Contracts.Shared.Crud.GetDetails;

namespace Se.Web.Client;

public interface IWebApiClient
{
    Task<ProductGetDetailsResponse> GetDetailsAsync(GetDetailsRequest request, CancellationToken ct = default);
}
