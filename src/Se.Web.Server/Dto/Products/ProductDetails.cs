﻿using Se.Web.Server.Dto.Crud;

namespace Se.Web.Server.Dto.Products;

public record ProductDetails(int Id, string Name) : GetDetailsResponse;

