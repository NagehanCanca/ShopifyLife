using Microsoft.EntityFrameworkCore;
using ShopifyLite.Models; // Models namespace'i eklenmeli

namespace ShopifyLite.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<Item> Items { get; set; } // Item sınıfınızın DbSet'i

        // Diğer DbSet'ler eklenebilir, gerektiği şekilde düzenleyebilirsiniz.
    }
}
