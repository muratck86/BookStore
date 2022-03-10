using Microsoft.EntityFrameworkCore;
using WebApi5.Entities;

namespace WebApi5.DbOperations
{
    public interface IBookStoreDbContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<Genre> Genres { get; set; }
        int SaveChanges();
        
    }
}
