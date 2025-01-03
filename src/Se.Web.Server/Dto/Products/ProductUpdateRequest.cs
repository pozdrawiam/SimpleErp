using System.ComponentModel.DataAnnotations;

namespace Se.Web.Server.Dto.Products;

public record ProductUpdateRequest([Required, MinLength(4)] string? Name) : IdRequestBase;
