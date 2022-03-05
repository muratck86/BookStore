using System;
using System.Linq;
using WebApi5.DbOperations;
using WebApi5.Common;
using AutoMapper;

namespace WebApi5.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId;
        public GetBookDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Where(book => book.Id == BookId).SingleOrDefault();
            if (book == null)
                throw new NullReferenceException("Kitap bulunamadı");
            BookDetailViewModel result = _mapper.Map<BookDetailViewModel>(book); //new BookDetailViewModel();
            // result.Title = book.Title;
            // result.PageCount = book.PageCount;
            // result.Genre = ((GenreEnum)book.GenreId).ToString();
            // result.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
            return result;
        }

    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }

}