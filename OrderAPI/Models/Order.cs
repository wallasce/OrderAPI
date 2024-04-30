using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderAPI.Models;

[Table("Orders")]
public class Order
{
    [Key]
    public int OrderId { get; set; }

    [Required]
    [StringLength(15)]
    public string CPF { get; set; }

    [Required]
    public int Table { get; set; }
    
    public enum States
    {
        Open,
        Close,
    }

    [Required]
    public States State { get; set; }

    [Required]
    [Column(TypeName = "decimal(15,2)")]
    public float CurrentOrderBalance { get; set; }

    [Required]
    public DateOnly Date { get; set; }

    public ICollection<Item> Items { get; set; }

    public Order()
    {
        Items = new Collection<Item>();
    }

}
