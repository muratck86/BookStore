using System.Linq;
using WebApi5.DbOperations;
using System.Collections.Generic;
using AutoMapper;
using WebApi5.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApi5.Application.BookOperations.Queries.GetBooks
{
    public class GetBooksByGenreQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public int Id;
        public GetBooksByGenreQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<BooksByGenreViewModel> Handle()
        {
            var bookList = _dbContext.Books
                .Include(x => x.Author)
                .Where(x => x.GenreId == Id)
                .OrderBy(x => x.Id)
                .ToList<Book>();

            List<BooksByGenreViewModel> vm = _mapper.Map<List<BooksByGenreViewModel>>(bookList); 
            return vm;
        }
        
    }

    public class BooksByGenreViewModel
    {
        public int Id {get; set;}
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Author { get; set; }
    }
}