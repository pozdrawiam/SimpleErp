using System.ComponentModel.DataAnnotations.Schema;

namespace Se.Domain.Features.Products;

[Table("Products")]
public class ProductEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}
