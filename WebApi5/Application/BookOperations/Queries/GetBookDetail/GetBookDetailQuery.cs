using System;
using System.Linq;
using WebApi5.DbOperations;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WebApi5.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId;
        public GetBookDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books
                .Include(x => x.Genre)
                .Include(x => x.Author)
                .Where(book => book.Id == BookId)
                .SingleOrDefault();
            if (book == null)
                throw new NullReferenceException("Kitap bulunamadı");
            BookDetailViewModel result = _mapper.Map<BookDetailViewModel>(book); 
            return result;
        }

    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
    }

}