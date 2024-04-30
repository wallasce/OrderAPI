using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OrderAPI.Models;

[Table("Items")]
public class Item
{
    [Key]
    public int ItemID { get; set; }

    [Required]
    public int ProductId { get; set; }

    [JsonIgnore]
    public Product Product { get; set; }

    [Required]
    public int Quantity { get; set; }

    [StringLength(200)]
    public string Observation { get; set; }

    public enum States
    {
        PREPARING,
        READY,
        DELIVERED,
    }

    [Required]
    public States State { get; set; }
    public int OrderId { get; set; }
    [JsonIgnore]
    public Order Order { get; set; }
}
