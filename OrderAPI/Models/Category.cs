using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderAPI.Models;

[Table("Categories")]
public class Category
{
    [Key]
    public int CategoryId { get; set; }
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
}
