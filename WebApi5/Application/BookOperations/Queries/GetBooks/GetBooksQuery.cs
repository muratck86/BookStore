using System.Linq;
using WebApi5.DbOperations;
using System.Collections.Generic;
using WebApi5.Common;
using AutoMapper;
using WebApi5.Entities;

namespace WebApi5.Application.BookOperations.Queries.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList<Book>();

            //Alt 3
            List<BooksViewModel> vm = _mapper.Map<List<BooksViewModel>>(bookList); //new List<BooksViewModel>();
            // Alt 2
            // foreach (var book in bookList)
            // {
            //     // vm.Add(_mapper.Map<BooksViewModel>(book));
             
            //     //Alt 1
            //     // vm.Add(new BooksViewModel()
            //     // {
            //     //     Id = book.Id,
            //     //     Title = book.Title,
            //     //     Genre = ((GenreEnum)book.GenreId).ToString(),
            //     //     PageCount = book.PageCount,
            //     //     PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy")
            //     // });
            // }
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
    }
}