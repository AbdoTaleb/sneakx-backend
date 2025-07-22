using Microsoft.EntityFrameworkCore;
using SneakX.API.Models;

namespace SneakX.API.Data
{
    public class SneakXContext : DbContext
    {
        public SneakXContext(DbContextOptions<SneakXContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; } = null!;
    }
}
