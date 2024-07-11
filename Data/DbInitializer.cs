using Rozetka.Models;

namespace Rozetka.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Categories.Any())
            {
                return; // DB has been seeded
            }

            var categories = new Category[]
            {
            new Category { Name = "Electronics" },
            new Category { Name = "Books" }
            };

            foreach (Category c in categories)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();
        }
    }
}
