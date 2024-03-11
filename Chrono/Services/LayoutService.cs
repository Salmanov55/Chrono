using Chrono.DAL;
using Chrono.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Chrono.Services
{
    public class LayoutService
    {
        readonly AppDbContext _context;
        readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LayoutService(AppDbContext context, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor = null)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GetUserName()
        {
            string username = string.Empty;
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {

                    var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                    username = user.FullName;

                return username;
            }


            return username;
        }

        public async Task<List<Product>> GetProductsNav()
        {
            return await _context.Products
                .Include(p => p.ProductImages)
                .Take(7).OrderByDescending(p => p.Id)
                .ToListAsync();
        }

    }
}
