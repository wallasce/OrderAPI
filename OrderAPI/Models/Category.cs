using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OrderAPI.Models;

[Table("Categories")]
public class Category
{
    [Key]
    public int CategoryId { get; set; }
    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [JsonIgnore]
    public ICollection<Product> Products { get; set; }
}
