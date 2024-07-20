namespace Rozetka.Data.Entity
{
    public class Review
    {
        public Guid Id { get; set; }
        public Guid? ProductId { get; set; }
        public Product? Product { get; set; }
        public Guid? UserId { get; set; }
        public User? User { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
    }
}
