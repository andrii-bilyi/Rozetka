namespace Rozetka.Data.Entity
{
    public class Subcategory
    {
        public int Id { get; set; }
        public required String Name { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
