using System.ComponentModel.DataAnnotations;
using Se.Contracts.Shared.Crud.Update;

namespace Se.Contracts.Features.Products;

public record ProductUpdateRequest(int Id, [Required, MinLength(4)] string? Name) : UpdateRequestBase(Id);
