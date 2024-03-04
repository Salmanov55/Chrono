using Microsoft.AspNetCore.Identity;

namespace Chrono.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
