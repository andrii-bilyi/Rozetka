using Microsoft.AspNetCore.Mvc.Rendering;
using Rozetka.Data.Entity;

namespace Rozetka.Models.ViewModels.Products
{
    public class ProductViewModel
    {
        public Product Product { get; set; } = default!;
        public Brand Brand { get; set; } = default!;
        public LinkedList<ProductImage> ProductImages { get; set; } = default!;
        public LinkedList<Review> Reviews { get; set; } = default!;
    }
}
