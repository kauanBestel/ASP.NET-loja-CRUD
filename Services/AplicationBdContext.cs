using loja.Models;
using Microsoft.EntityFrameworkCore;

namespace loja.Services
{
    public class AplicationBdContext : DbContext
    {
        public AplicationBdContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }    
    }
}
