using System.ComponentModel.DataAnnotations;

namespace Se.Web.Server.Dto.Products;

public record ProductsUpdateRequest([Required, MinLength(4)] string? Name) : IdRequestBase;
