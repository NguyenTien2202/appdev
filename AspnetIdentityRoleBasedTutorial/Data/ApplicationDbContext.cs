using AspnetIdentityRoleBasedTutorial.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspnetIdentityRoleBasedTutorial.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Jewelry> Jewelries { get; set; }
        public DbSet<JewelryType> JewelryTypes { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<AspnetIdentityRoleBasedTutorial.Models.Category> Category { get; set; } = default!;
    }
}