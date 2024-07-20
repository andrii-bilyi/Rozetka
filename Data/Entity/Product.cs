namespace Rozetka.Data.Entity
{
    public class Product
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Brand { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }
        //public int? CategoryId { get; set; }        
        //public Category Category { get; set; }
        public int? QuantityInStock { get; set; }
        public ICollection<ProductImage>? ProductImages { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
