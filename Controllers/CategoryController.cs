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
    public class CategoryController : Controller
    {
        private readonly AppDbContext context;

        public CategoryController(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Category> allcategories = await context.Categories.OrderBy(x => x.DisplayOrder).ToListAsync();
            return View(allcategories);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {    
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category obj)
        {

            if(ModelState.IsValid)
            {
                await context.AddAsync(obj);
                await context.SaveChangesAsync();
                TempData["success"] = "Record Created";
                return RedirectToAction("Index" , "Category");
            }
            
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            if(id == 0 || id == null)
            {
                return NotFound();
            }

            var obj = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if(obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            context.Categories.Update(category);
            await context.SaveChangesAsync();

            TempData["success"] = "Record Updated";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();            
            }

            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if(category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Category category)
        {
            if(category == null)
            {
                return NotFound();
            }

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            TempData["success"] = "Record Deleted";
            return RedirectToAction("Index");
        }
    }
}