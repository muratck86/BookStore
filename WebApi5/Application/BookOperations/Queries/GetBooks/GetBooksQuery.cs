using System.Linq;
using WebApi5.DbOperations;
using System.Collections.Generic;
using AutoMapper;
using WebApi5.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApi5.Application.BookOperations.Queries.GetBooks
{
    public class GetBooksQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBooksQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _dbContext.Books
                .Include(x => x.Genre)
                .Include(x => x.Author)
                .OrderBy(x => x.Id)
                .ToList<Book>();

            List<BooksViewModel> vm = _mapper.Map<List<BooksViewModel>>(bookList); 
            return vm;
        }
        
    }

    public class BooksViewModel
    {
        public int Id {get; set;}
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
    }
}