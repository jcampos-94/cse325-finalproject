using System.ComponentModel.DataAnnotations;

namespace cse325_finalproject.Models;

public class Product
{
    public int Id { get; set; }


    [Required]
    public string Name { get; set; } = string.Empty;


    public string? Description { get; set; }


    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }


    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }

    // Foreign key linking the product to the user who created it.
    public string UserId { get; set; } = string.Empty;

    // Navigation property
    public ApplicationUser? User { get; set; }

    // Foreign key linking the product to its category.
    public int CategoryId { get; set; }

    // Navigation property for the product's category.
    public Category? Category { get; set; }
}