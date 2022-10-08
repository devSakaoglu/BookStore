using BookStore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Context
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
    }

}
