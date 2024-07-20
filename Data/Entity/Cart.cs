namespace Rozetka.Data.Entity
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid? IdProduct { get; set; }
        public int Quantity { get; set; } = 1;
        public Guid? UserId { get; set; }
        //public User? User { get; set; }
    }
}
