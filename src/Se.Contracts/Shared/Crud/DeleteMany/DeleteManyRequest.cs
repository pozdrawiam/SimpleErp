using System.ComponentModel.DataAnnotations;
using Se.Contracts.Shared.Cqs;

namespace Se.Contracts.Shared.Crud.DeleteMany;

public record DeleteManyRequest : ICmd
{
    [Required]
    public IReadOnlyCollection<int>? Ids { get; init; }
}
