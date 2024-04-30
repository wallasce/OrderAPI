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
    public long CPF { get; set; }

    [Required]
    public int Tabble { get; set; }
    
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
