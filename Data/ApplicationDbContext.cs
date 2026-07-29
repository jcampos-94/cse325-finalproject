using Microsoft.EntityFrameworkCore;
using cse325_finalproject.Models;

namespace cse325_finalproject.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }


    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }
}