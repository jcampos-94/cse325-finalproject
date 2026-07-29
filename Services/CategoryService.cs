using Microsoft.EntityFrameworkCore;
using cse325_finalproject.Data;
using cse325_finalproject.Models;

namespace cse325_finalproject.Services;

public class CategoryService
{
    private readonly ApplicationDbContext _context;

    public CategoryService(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }


    public async Task AddCategoryAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
    }


    public async Task DeleteCategoryAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}