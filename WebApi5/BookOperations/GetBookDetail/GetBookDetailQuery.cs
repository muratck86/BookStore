using System;
using System.Linq;
using WebApi5.DbOperations;
using WebApi5.Common;

namespace WebApi5.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId;
        public GetBookDetailQuery(BookStoreDbContext context)
        {
            _dbContext = context;
        }

        public BookByIdViewModel Handle()
        {
            var book = _dbContext.Books.Where(book => book.Id == BookId).SingleOrDefault();
            if (book == null)
                throw new NullReferenceException("Kitap bulunamadı");
            BookByIdViewModel result = new BookByIdViewModel();
            result.Title = book.Title;
            result.PageCount = book.PageCount;
            result.Genre = ((GenreEnum)book.GenreId).ToString();
            result.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
            return result;
        }

    }

    public class BookByIdViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }

}