using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderAPI.Models;

[Table("Products")]
public class Product
{
    [Key]
    public int ProductId { get; set; }
    
    [Required]
    public int CategoryId { get; set; }
    public Category Category { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    [StringLength(200)]
    public string Description { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(15,2)")]
    public float Price { get; set; }
    
    [Required]
    public int Serves { get; set; }
}
