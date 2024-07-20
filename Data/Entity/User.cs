namespace Rozetka.Data.Entity
{
    public class User
    {
        public Guid Id { get; set; }
        public String? FirstName { get; set; }
        public String? LastName { get; set; }
        public required String Phone { get; set; }
        public String? Email { get; set; }
        public required String PasswordSalt { get; set; }
        public required String PasswordDk { get; set; }
        public DateTime RegisterDt { get; set; }
        public DateTime? DeleteDt { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<WishList>? WishList { get; set; }
        public ICollection<Cart>? Cart { get; set; }
        public ICollection<ShoppingList>? ShoppingList { get; set; }
    }
}
