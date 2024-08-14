﻿namespace Rozetka.Data.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;       
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int? BrandId { get; set; }
        public Brand? Brand { get; set; }
        public int? ProductTypeId { get; set; }
        public ProductType? ProductType { get; set; }
        public int? ChildcategoryId { get; set; }
        public Childcategory? Childcategory { get; set; }
        //public int? CategoryId { get; set; }        
        //public Category Category { get; set; }
        public int? QuantityInStock { get; set; }
        public ICollection<ProductImage>? ProductImages { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
