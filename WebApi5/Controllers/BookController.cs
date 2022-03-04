using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using WebApi5.DbOperations;
using WebApi5.BookOperations.GetBooks;
using WebApi5.BookOperations.CreateBook;

namespace WebApi5.Controllers 
{
    [ApiController]
    [Route("[Controller]s")]
    public class BookController : ControllerBase 
    {
        private readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public Book GetById(int Id) 
        {
            var book = _context.Books.Where(book => book.Id == Id).SingleOrDefault();
            return book;
        }

        // [HttpGet]
        // public Book Get([FromQuery] int id) {
        //     var book = BookList.Where(book => book.Id == id).SingleOrDefault();
        //     return book;
        // }

        [HttpPost]
        public IActionResult AddBook ([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }


            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = _context.Books.SingleOrDefault<Book>(x => x.Id == updatedBook.Id);
            if(book is null)
            {
                return BadRequest();
            }
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

            _context.SaveChanges();

            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault<Book>(x => x.Id == id);
            if (book is not null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}