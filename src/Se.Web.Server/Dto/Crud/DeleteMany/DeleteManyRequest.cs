using System.ComponentModel.DataAnnotations;

namespace Se.Web.Server.Dto.Crud.DeleteMany;

public record DeleteManyRequest
{
    [Required]
    public IReadOnlyCollection<int>? Ids { get; init; }
}
