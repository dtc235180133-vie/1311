using _1311.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductCategoryMVC.Models;

namespace _1311.Controllers
{
    public class ProductController : Controller
    {
        // Dữ liệu mẫu lưu tạm trên RAM (không cần SQL Server)
        private static List<Category> _categories = new List<Category>
        {
            new Category { CategoryId = 1, CategoryName = "Điện thoại" },
            new Category { CategoryId = 2, CategoryName = "Laptop" },
            new Category { CategoryId = 3, CategoryName = "Phụ kiện" }
        };

        private static List<Product> _products = new List<Product>
        {
            new Product { ProductId = 1, ProductName = "iPhone 15", ProductPrice = 20000, CategoryId = 1 },
            new Product { ProductId = 2, ProductName = "MacBook Air", ProductPrice = 25000, CategoryId = 2 }
        };

        // 1. DANH SÁCH (Sử dụng ProductViewModel bạn đã tạo)
        public IActionResult Index()
        {
            var viewModel = _products.Select(p => new ProductViewModel
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                ProductPrice = p.ProductPrice,
                CategoryName = _categories.FirstOrDefault(c => c.CategoryId == p.CategoryId)?.CategoryName
            }).ToList();

            return View(viewModel);
        }

        // 2. THÊM MỚI (GET & POST)
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_categories, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.ProductId = _products.Any() ? _products.Max(p => p.ProductId) + 1 : 1;
                _products.Add(product);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(_categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // 3. SỬA (GET & POST)
        public IActionResult Edit(int id)
        {
            var product = _products.FirstOrDefault(p => p.ProductId == id);
            if (product == null) return NotFound();

            ViewBag.Categories = new SelectList(_categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var existing = _products.FirstOrDefault(p => p.ProductId == product.ProductId);
                if (existing != null)
                {
                    existing.ProductName = product.ProductName;
                    existing.ProductPrice = product.ProductPrice;
                    existing.CategoryId = product.CategoryId;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(_categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // 4. XÓA (GET & POST)
        public IActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.ProductId == id);
            if (product == null) return NotFound();

            ViewBag.CategoryName = _categories.FirstOrDefault(c => c.CategoryId == product.CategoryId)?.CategoryName;
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _products.FirstOrDefault(p => p.ProductId == id);
            if (product != null)
            {
                _products.Remove(product);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}