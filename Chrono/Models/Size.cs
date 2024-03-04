using Chrono.Models.Base;

namespace Chrono.Models
{
    public class Size : BaseEntity
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
