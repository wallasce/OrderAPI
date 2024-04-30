using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OrderAPI.Models;

[Table("Products")]
public class Product
{
    [Key]
    public int ProductId { get; set; }
    
    [Required]
    public int CategoryId { get; set; }
    
    [JsonIgnore]
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
