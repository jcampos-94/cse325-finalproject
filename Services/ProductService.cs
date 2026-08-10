using cse325_finalproject.Data;
using cse325_finalproject.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace cse325_finalproject.Services;

public class ProductService
{
  private readonly ApplicationDbContext _context;
  private readonly AuthenticationStateProvider _authenticationStateProvider;


  public ProductService(
      ApplicationDbContext context,
      AuthenticationStateProvider authenticationStateProvider)
  {
    _context = context;
    _authenticationStateProvider = authenticationStateProvider;
  }

  // Get the ID of the currently authenticated user.
  private async Task<string?> GetCurrentUserIdAsync()
  {
    var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();

    var user = authState.User;

    if (user.Identity?.IsAuthenticated == true)
    {
      return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    return null;
  }


  public async Task<List<Product>> GetProductsAsync()
  {
    var userId = await GetCurrentUserIdAsync();

    if (userId == null)
    {
      return new List<Product>();
    }

    return await _context.Products
        .Where(p => p.UserId == userId)
        .Include(p => p.Category)
        .ToListAsync();
  }


  public async Task<Product?> GetProductAsync(int id)
  {
    var userId = await GetCurrentUserIdAsync();

    if (userId == null)
    {
      return null;
    }

    /// Only return products owned by the logged-in user.
    return await _context.Products
        .Where(p => p.UserId == userId)
        .Include(p => p.Category)
        .FirstOrDefaultAsync(p => p.Id == id);
  }


  public async Task<List<Product>> SearchProductsAsync(string searchTerm)
  {
    var userId = await GetCurrentUserIdAsync();

    if (userId == null)
    {
      return new List<Product>();
    }

    // Restrict searches to products owned by the logged-in user.
    var query = _context.Products
        .Where(p => p.UserId == userId)
        .Include(p => p.Category)
        .AsQueryable();

    if (!string.IsNullOrWhiteSpace(searchTerm))
    {
      var term = searchTerm.Trim().ToLower();

      query = query.Where(p =>
          p.Name.ToLower().Contains(term) ||
          (p.Description != null && p.Description.ToLower().Contains(term)) ||
          (p.Category != null && p.Category.Name.ToLower().Contains(term)));
    }

    return await query.ToListAsync();
  }


  public async Task AddProductAsync(Product product)
  {
    var userId = await GetCurrentUserIdAsync();

    if (userId == null)
    {
      return;
    }

    // Associate the new product with the logged-in user.
    product.UserId = userId;

    _context.Products.Add(product);

    await _context.SaveChangesAsync();
  }


  public async Task UpdateProductAsync(Product product)
  {
    var userId = await GetCurrentUserIdAsync();

    if (userId == null)
    {
      return;
    }

    // Find the product only if it belongs to the logged-in user.
    var existingProduct = await _context.Products
        .FirstOrDefaultAsync(p =>
            p.Id == product.Id &&
            p.UserId == userId);


    if (existingProduct != null)
    {
      existingProduct.Name = product.Name;

      existingProduct.Description = product.Description;

      existingProduct.Price = product.Price;

      existingProduct.Quantity = product.Quantity;

      existingProduct.CategoryId = product.CategoryId;


      await _context.SaveChangesAsync();
    }
  }


  public async Task DeleteProductAsync(int id)
  {
    var userId = await GetCurrentUserIdAsync();

    if (userId == null)
    {
      return;
    }

    // Find the product only if it belongs to the logged-in user.
    var product = await _context.Products
        .FirstOrDefaultAsync(p =>
            p.Id == id &&
            p.UserId == userId);


    if (product != null)
    {
      _context.Products.Remove(product);

      await _context.SaveChangesAsync();
    }
  }
}