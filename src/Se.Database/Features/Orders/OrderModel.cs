using System.ComponentModel.DataAnnotations.Schema;

namespace Se.Database.Features.Orders;

[Table("Orders")]
public class OrderModel
{
    public int Id { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public string Note { get; set; } = "";
}
