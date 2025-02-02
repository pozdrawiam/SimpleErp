using System.ComponentModel.DataAnnotations;
using Se.Contracts.Shared.Crud.Create;

namespace Se.Contracts.Features.Products;

public record ProductCreateRequest([Required, MinLength(4)] string? Name) : CreateRequestBase;
