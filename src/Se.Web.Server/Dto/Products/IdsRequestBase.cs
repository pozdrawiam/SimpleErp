using System.ComponentModel.DataAnnotations;

namespace Se.Web.Server.Dto.Products;

public abstract record IdsRequestBase
{
    [Required]
    public IReadOnlyCollection<int>? Ids { get; init; }
}
