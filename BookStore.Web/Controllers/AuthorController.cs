using BookStore.Data.Context;
using BookStore.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
namespace BookStore.Web.Controllers
{
    public class AuthorController : Controller
    {
        public readonly DataContext _context;
        public AuthorController(DataContext context)
        {
            _context = context;

        }

        public async Task<IActionResult> Index()
        {
            var authors = await _context.Authors.ToListAsync();
            return View(authors);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Author auth)
        {
            var check = await _context.Authors.FirstOrDefaultAsync(c => c.AuthorName == auth.AuthorName);

            if (check == null)
            {
                await _context.Authors.AddAsync(new Author() { AuthorName = auth.AuthorName, AuthorBirthDate = auth.AuthorBirthDate });
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        public async Task<Author> Read(string search)
        {
            var sellect = await _context.Authors.FirstOrDefaultAsync(c => c.AuthorName == search);
            return sellect;
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            var authorInDB = await _context.Authors.FindAsync(Id);
            _context.Authors.Remove(authorInDB);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.Authors.FindAsync(id);
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Author author)
        {
            var newAuthor = await _context.Authors.FindAsync(author.Id);
            if (newAuthor is not null &&(newAuthor.AuthorBirthDate==author.AuthorBirthDate && newAuthor.AuthorName==author.AuthorName))
            {
                return RedirectToAction("Index");
            }
            else
            {
                newAuthor.AuthorBirthDate = author.AuthorBirthDate;
                newAuthor.AuthorName = author.AuthorName;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");

        }
    }
}
