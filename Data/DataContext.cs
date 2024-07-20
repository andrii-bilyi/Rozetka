using Microsoft.EntityFrameworkCore;
using Rozetka.Data.Entity;

//using RozetkaDatabase.Models;

namespace Rozetka.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<WishList> WishList { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ShoppingList> ShoppingList { get; set; }
        public DbSet<LoginJournalItem> LoginJournal { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Rozetka");
        }
    }
}
