using System.Linq;
using WebApi5.DbOperations;
using System.Collections.Generic;
using AutoMapper;
using WebApi5.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApi5.Application.BookOperations.Queries.GetBooks
{
    public class GetBooksByAuthorQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public int Id;
        public GetBooksByAuthorQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<BooksByAuthorViewModel> Handle()
        {
            var bookList = _dbContext.Books
                .Include(x => x.Genre)
                .Where(x => x.AuthorId == Id)
                .OrderBy(x => x.Id)
                .ToList<Book>();

            List<BooksByAuthorViewModel> vm = _mapper.Map<List<BooksByAuthorViewModel>>(bookList); 
            return vm;
        }
        
    }

    public class BooksByAuthorViewModel
    {
        public int Id {get; set;}
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}