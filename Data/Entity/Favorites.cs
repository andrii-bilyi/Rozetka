namespace Rozetka.Data.Entity
{
    public class Favorites
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; } = default!;
        public User? User { get; set; }
        public Product? Product { get; set; }
    }
}
