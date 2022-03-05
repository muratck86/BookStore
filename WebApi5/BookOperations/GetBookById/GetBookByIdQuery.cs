using System;
using System.Linq;
using WebApi5.DbOperations;
using System.Collections.Generic;
using WebApi5.Common;

namespace WebApi5.BookOperations.GetBookById
{
    public class GetBookByIdQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public GetBookByIdQuery(BookStoreDbContext context)
        {
            _dbContext = context;
        }

        public BookByIdViewModel Handle(int Id)
        {
            var book = _dbContext.Books.Where(book => book.Id == Id).SingleOrDefault();
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