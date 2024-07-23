namespace Rozetka.Data.Entity
{
    public class Brand
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public ICollection<Product>? Product { get; set; }
    }
}
