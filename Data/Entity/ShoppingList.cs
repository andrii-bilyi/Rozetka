namespace Rozetka.Data.Entity
{
    public class ShoppingList //список покупок
    {
        public Guid Id { get; set; }
        public ICollection<Cart>? Cart { get; set; }
        public DateTime DatePurchase { get; set; }
        public decimal? TotalPrice { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
