using BookStore.Data.Context;
using BookStore.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Web.Controllers;

namespace BookStore.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly DataContext _context;

        public BookController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var books = (await _context.Books.ToListAsync(), new Book());


            return View(books);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {

            var bookInDB = await _context.Books.FirstOrDefaultAsync(x => x.Id == Id);
            _context.Books.Remove(bookInDB);
            await _context.SaveChangesAsync();



            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _context.Books.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
            var categories = await _context.Categories.ToListAsync();
            var model = (book, categories);
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(Book Book, Category category)
        {
            var book = await _context.Books.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == Book.Id);
            book.BookName = Book.BookName;
            if (category.CategoryName is not "Empty")
            {
                Category newCategory;
                var categoryEntry = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryName == category.CategoryName);
                newCategory = categoryEntry ?? category;
                book.Categories.Add(newCategory);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Edit", "Book", book.Id);

        }
        public async Task<IActionResult> BookCategoryDelete(Guid caregoryId, int bookID)
        {
            var bookCategoryData = await _context.Books.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == bookID);
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == caregoryId);
            bookCategoryData.Categories.Remove(category);
            await _context.SaveChangesAsync();
            ;

            return RedirectToAction("Edit", "Book", bookCategoryData);
        }

    }
}
