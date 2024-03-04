using Chrono.Models.Base;

namespace Chrono.Models
{
    public class Rating : BaseEntity
    {
        public byte Vote { get; set; }
        public AppUser User { get; set; }
        public int ProductId { get; set; }
    }
}
