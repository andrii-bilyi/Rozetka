namespace Rozetka.Data.Entity
{
    public class Cart
    {
        public int Id { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { get; set; } = 1;
        public string UserId { get; set; } = default!;
        //public User? User { get; set; }
    }
}
