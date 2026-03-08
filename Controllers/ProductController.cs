using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace E_Commerce.Controllers
{
    [Route("[controller] / [action]")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly AppDbContext context;

        public ProductController(ILogger<ProductController> logger, AppDbContext context)
        {
            this.context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Product> allProducts = await context.Products.ToListAsync();
            return View(allProducts);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product obj)
        {
            if (ModelState.IsValid)
            {
                await context.Products.AddAsync(obj);
                await context.SaveChangesAsync();

                TempData["success"] = "Product Create!";
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                context.Products.Update(product);
                await context.SaveChangesAsync();

                TempData["success"] = "Product Updated!";
                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Product product)
        {
            context.Products.Remove(product);

            await context.SaveChangesAsync();

            TempData["success"] = "Product Deleted!";
            return RedirectToAction("Index");
        }
    }
}