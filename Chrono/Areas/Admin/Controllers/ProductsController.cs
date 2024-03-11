using Chrono.DAL;
using Chrono.Extensions;
using Chrono.Models;
using Chrono.ViewModels.ProductsVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Chrono.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;

        public ProductsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Color)
                .Include(p => p.Material)
                .Include(p => p.Size)
                .ToListAsync();
            return View(await appDbContext);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Color)
                .Include(p => p.Material)
                .Include(p => p.Size)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Name");
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name");
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM productVM)
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", productVM.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", productVM.CategoryId);
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Name", productVM.ColorId);
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name", productVM.MaterialId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Name", productVM.SizeId);

            if (!ModelState.IsValid)
            {
                return View(productVM);
            }

            var existProd = _context.Products.Any(x => x.Name == productVM.Name);
            if (existProd)
            {
                ModelState.AddModelError("Name", "Bu adla product movcuddu !");
                return View(productVM);
            }
            List<ProductImage> images = new();
            foreach (var item in productVM.Photos)
            {
                if (item.CheckImageSize(5))
                {
                    ModelState.AddModelError("Photos", "Size duzgun deil !");
                    View();
                }
                if (!item.CheckImageType())
                {
                    ModelState.AddModelError("Photos", "Only image !");
                    View();
                }
                ProductImage image = new();

                image.ImageUrl = item.SaveImage(_env, "Client/assets/img");
                images.Add(image);
            }

            Product product = new Product()
            {
                Name = productVM.Name,
                BrandId = productVM.BrandId,
                CategoryId = productVM.CategoryId,
                SizeId = productVM.SizeId,
                ColorId = productVM.ColorId,
                MaterialId = productVM.MaterialId,
                Price = productVM.Price,
            };

            product.ProductImages = images;

            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(int? id, int? imgId)
        {

            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }



            if (imgId != null)
            {
                var image = product.ProductImages.FirstOrDefault(p => p.Id == imgId);
                if (image != null)
                {
                    product.ProductImages.Remove(image);
                    _context.SaveChanges();
                }

                if (System.IO.File.Exists(Path.Combine(_env.WebRootPath, "Client/assets/img", image.ImageUrl)))
                {
                    System.IO.File.Delete(Path.Combine(_env.WebRootPath, "Client/assets/img", image.ImageUrl));
                }
            }



            ProductEditVM vm = new ProductEditVM()
            {
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                SizeId = product.SizeId,
                ColorId = product.ColorId,
                MaterialId = product.MaterialId,
                Price = product.Price,
                Name = product.Name,
                ProductImages = product.ProductImages,
            };


            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Name", product.ColorId);
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name", product.MaterialId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Name", product.SizeId);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductEditVM productVM)
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", productVM.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", productVM.CategoryId);
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Name", productVM.ColorId);
            ViewData["MaterialId"] = new SelectList(_context.Materials, "Id", "Name", productVM.MaterialId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Name", productVM.SizeId);


            Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (id != productVM.Id || product == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(productVM);
            }

            if (productVM.Photos.Length > 0)
            {
                foreach (var item in productVM.Photos)
                {
                    if (item.CheckImageSize(5))
                    {
                        ModelState.AddModelError("Photos", "Size duzgun deil !");
                        View();
                    }
                    if (!item.CheckImageType())
                    {
                        ModelState.AddModelError("Photos", "Only image !");
                        View();
                    }
                    ProductImage image = new();

                    image.ImageUrl = item.SaveImage(_env, "Client/assets/img");
                    if (product.ProductImages == null)
                    {
                        product.ProductImages = new();
                    }
                    product.ProductImages.Add(image);
                }
            }

            product.Price = productVM.Price;
            product.CategoryId = productVM.CategoryId;
            product.SizeId = productVM.SizeId;
            product.ColorId = productVM.ColorId;
            product.Name = productVM.Name;
            product.MaterialId = productVM.MaterialId;
            product.BrandId = productVM.BrandId;
            
            _context.SaveChanges();

            return RedirectToAction("index", "products");
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Color)
                .Include(p => p.Material)
                .Include(p => p.Size)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'AppDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
