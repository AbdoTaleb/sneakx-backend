using Microsoft.EntityFrameworkCore;
using SneakX.API.Models;

namespace SneakX.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Hoodie> Hoodies { get; set; }
    }
}