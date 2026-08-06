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

        return await _context.Products
            .Where(p => p.UserId == userId)
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }


    public async Task AddProductAsync(Product product)
    {
        var userId = await GetCurrentUserIdAsync();

        if (userId == null)
        {
            return;
        }

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