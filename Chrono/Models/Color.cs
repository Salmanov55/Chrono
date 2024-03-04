using Chrono.Models.Base;

namespace Chrono.Models
{
    public class Color : BaseEntity
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
