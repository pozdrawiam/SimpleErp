using System.ComponentModel.DataAnnotations.Schema;

namespace Se.Database.Models;

[Table("Orders")]
public class OrderModel
{
    public int Id { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public string Note { get; set; } = "";
}
