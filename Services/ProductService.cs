using cse325_finalproject.Data;
using cse325_finalproject.Models;
using Microsoft.EntityFrameworkCore;

namespace cse325_finalproject.Services;

public class ProductService
{
    private readonly ApplicationDbContext _context;


    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<List<Product>> GetProductsAsync()
    {
        return await _context.Products
            .Include(p => p.Category)
            .ToListAsync();
    }


    public async Task<Product?> GetProductAsync(int id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }


    public async Task AddProductAsync(Product product)
    {
        _context.Products.Add(product);

        await _context.SaveChangesAsync();
    }


    public async Task UpdateProductAsync(Product product)
    {
        var existingProduct = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == product.Id);


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
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);


        if (product != null)
        {
            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
        }
    }
}