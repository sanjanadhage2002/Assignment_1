using Assignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;

        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // Get the list of products
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return View(products);
        }

        // Show the Add Product form
        public IActionResult Add()
        {
            return View();
        }

        // Handle the Add Product form submission
        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.AddProductAsync(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // Show the Edit Product form
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // Handle the Edit Product form submission
        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.UpdateProductAsync(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // Handle the Delete Product action
        public async Task<IActionResult> Delete(int id)
        {
            await _productRepository.DeleteProductAsync(id);
            return RedirectToAction("Index");
        }
    }
}
