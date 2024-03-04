using Chrono.Models.Base;

namespace Chrono.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public List<Rating> Ratings { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int MaterialId { get; set; }
        public Material Material { get; set; }
        public int ColorId { get; set; }
        public Color Color { get; set; }
        public int SizeId { get; set; }
        public Size Size { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}
