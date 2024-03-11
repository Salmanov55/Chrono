using Chrono.Models;
using System.ComponentModel.DataAnnotations;

namespace Chrono.ViewModels.ProductsVM
{
    public class ProductEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int BrandId { get; set; }
        [Required]
        public int MaterialId { get; set; }
        [Required]
        public int ColorId { get; set; }
        [Required]
        public int SizeId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public IFormFile[] Photos { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}
