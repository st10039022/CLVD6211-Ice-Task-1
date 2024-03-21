using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CloudIceTaskOne.Models;

namespace CloudIceTaskOne.Controllers
{
    public class ControllerTwo : Controller
    {
        public class ProductController : Controller
        {
            private readonly List<Product> _products; 

            public ProductController()
            {
                
                _products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 1000m },
                new Product { Id = 2, Name = "Product 2", Price = 750m },
                new Product { Id = 3, Name = "Product 3", Price = 299.99m }
            };
            }

            public IActionResult Index()
            {
                return View(_products); // Pass the list of products to the view
            }

            public IActionResult Details(int id)
            {
                var product = _products.Find(p => p.Id == id);
                if (product == null)
                {
                    return NotFound(); // Return 404 if product not found
                }
                return View(product);
            }

            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Create(Product product)
            {
                if (ModelState.IsValid)
                {
                    // Add product to the list
                    _products.Add(product);
                    return RedirectToAction(nameof(Index));
                }
                return View(product);
            }

            public IActionResult Edit(int id)
            {
                var product = _products.Find(p => p.Id == id);
                if (product == null)
                {
                    return NotFound(); // Return 404 if product not found
                }
                return View(product);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Edit(int id, Product product)
            {
                if (id != product.Id)
                {
                    return NotFound(); // Return 404 if IDs don't match
                }

                if (ModelState.IsValid)
                {
                    // Update product in the list
                    var existingProduct = _products.Find(p => p.Id == id);
                    existingProduct.Name = product.Name;
                    existingProduct.Price = product.Price;
                    return RedirectToAction(nameof(Index));
                }
                return View(product);
            }

            public IActionResult Delete(int id)
            {
                var product = _products.Find(p => p.Id == id);
                if (product == null)
                {
                    return NotFound(); // Return 404 if product not found
                }
                return View(product);
            }

            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public IActionResult DeleteConfirmed(int id)
            {
                var product = _products.Find(p => p.Id == id);
                if (product == null)
                {
                    return NotFound(); // Return 404 if product not found
                }
                _products.Remove(product); // Remove product from list 
                return RedirectToAction(nameof(Index));
            }
            public IActionResult Search(string query)
            {
                var results = _products.Where(p => p.Name.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
                return View("Index", results);
            }
        }
    }
}