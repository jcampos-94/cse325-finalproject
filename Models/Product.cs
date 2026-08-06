using System.ComponentModel.DataAnnotations;

namespace cse325_finalproject.Models;

public class Product
{
    public int Id { get; set; }


    [Required]
    public string Name { get; set; } = string.Empty;


    public string? Description { get; set; }


    [Required]
    public decimal Price { get; set; }


    [Required]
    public int Quantity { get; set; }

    // Foreign key to the logged-in user
    public string UserId { get; set; } = string.Empty;

    // Navigation property
    public ApplicationUser? User { get; set; }

    public int CategoryId { get; set; }


    public Category? Category { get; set; }
}