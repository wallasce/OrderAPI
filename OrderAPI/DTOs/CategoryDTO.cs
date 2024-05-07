using System.ComponentModel.DataAnnotations;

namespace OrderAPI.DTOs;

public class CategoryDTO
{
    public int CategoryId { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }
}
