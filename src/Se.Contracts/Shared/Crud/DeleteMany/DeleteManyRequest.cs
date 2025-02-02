using System.ComponentModel.DataAnnotations;

namespace Se.Contracts.Shared.Crud.DeleteMany;

public record DeleteManyRequest
{
    [Required]
    public IReadOnlyCollection<int>? Ids { get; init; }
}
