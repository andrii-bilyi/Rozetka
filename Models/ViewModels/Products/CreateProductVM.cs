using Microsoft.AspNetCore.Mvc.Rendering;
using Rozetka.Data.Entity;

namespace Rozetka.Models.ViewModels.Products
{
    public class CreateProductVM
    {
        public SelectList Brands { get; set; } = default!;

        public SelectList Categories { get; set; } = default!;

        public Product Product { get; set; } = default!;
    }
}
