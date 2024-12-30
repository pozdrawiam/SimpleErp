using System.ComponentModel.DataAnnotations;

namespace Se.Web.Server.Dto.Products;

public record ProductsCreateRequest([Required, MinLength(4)] string? Name);
