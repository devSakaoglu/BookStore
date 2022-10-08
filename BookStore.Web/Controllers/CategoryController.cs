using BookStore.Data.Context;
using BookStore.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace BookStore.Web.Controllers
{
    public class CategoryController : Controller
    {
        public  DataContext _context;
        public CategoryController(DataContext context)
        {
            _context = context;

        }

        public async Task<bool> IsCategory(Category category)
        {
            bool check = false;

            if (category !=   null) { 
            check = await _context.Categories.FirstOrDefaultAsync(c=>c.CategoryName==category.CategoryName||c.Id==category.Id)!=null;
                return check;
                
            }

            return check;


        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();


            return View(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string categoryName)
        {
            //var check = await _context.Set<Category>().Where(x => x.CategoryName == categoryName).FirstOrDefaultAsync();

            var check = await _context.Categories.Include(c => c.Books).FirstOrDefaultAsync(c => c.CategoryName == categoryName);

            if (check == null)
            {

                await _context.Categories.AddAsync(new Category() { CategoryName = categoryName });
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {

            var categoryInDB = await _context.Categories.FirstOrDefaultAsync(x => x.Id == Id);
             _context.Categories.Remove(categoryInDB);
            await _context.SaveChangesAsync();



            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);

            return View(category);

        }


        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            var categoryInDb = await _context.Categories.FindAsync(category.Id);
            categoryInDb.CategoryName = category.CategoryName;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }















    }
}
