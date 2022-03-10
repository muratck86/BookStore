using System;
using System.Linq;
using WebApi5.DbOperations;

namespace WebApi5.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        public int BookId;
        public DeleteBookCommand(IBookStoreDbContext context)
        {
            _dbContext = context;
        }

        public void Handle()
        {
            var book = _dbContext.Books.Where(book => book.Id == BookId).SingleOrDefault();
            if (book == null)
                throw new NullReferenceException("Belirtilen Kitap Zaten Yok");
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }

    }
}