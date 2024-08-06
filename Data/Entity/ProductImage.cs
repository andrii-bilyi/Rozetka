﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Rozetka.Data.Entity
{
    public class ProductImage
    {
        public int Id { get; set; }
        public byte[]? ImageData { get; set; }
        public int? ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; } 
        //public string ImageUrl { get; set; } = default!;
    }
}
