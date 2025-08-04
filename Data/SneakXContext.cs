using Microsoft.EntityFrameworkCore;
using SneakX.API.Models;

namespace SneakX.API.Data
{
    public class SneakXContext : DbContext
    {
        public SneakXContext(DbContextOptions<SneakXContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Hoodie> Hoodies { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Hoodie>().ToTable("Hoodie"); 
        }
    }
}
