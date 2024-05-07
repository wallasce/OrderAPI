using OrderAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrderAPI.DTOs;

public class ProductDTO
{
    public int ProductId { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    [StringLength(200)]
    public string Description { get; set; }

    [Required]
    public float Price { get; set; }

    [Required]
    public int Serves { get; set; }
}
