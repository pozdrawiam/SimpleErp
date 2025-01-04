using System.ComponentModel.DataAnnotations;

namespace Se.Web.Server.Dto.Products;

public record IdsRequestBase
{
    [Required]
    public IReadOnlyCollection<int>? Ids { get; init; }
}
