namespace Rozetka.Data.Entity
{
    public class Category
    {
        public int Id { get; set; }
        public required String Name { get; set; }
        public ICollection<Subcategory>? Subcategory { get; set; }

        //public required ICollection<Product> Products { get; set; }
    }
}
