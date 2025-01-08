﻿using System.ComponentModel.DataAnnotations;
using Se.Web.Server.Dto.Crud;

namespace Se.Web.Server.Dto.Products;

public record ProductCreateRequest([Required, MinLength(4)] string? Name) : CreateRequestBase;
