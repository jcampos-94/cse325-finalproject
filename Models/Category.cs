namespace cse325_finalproject.Models;

public class Category
{
    public int Id { get; set; }

    // Name displayed when selecting or managing a category.
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    // Products assigned to this category.
    public List<Product> Products { get; set; } = new();
}