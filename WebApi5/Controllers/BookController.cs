using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using WebApi5.DbOperations;
using WebApi5.BookOperations.GetBooks;
using WebApi5.BookOperations.CreateBook;
using WebApi5.BookOperations.GetBookDetail;
using WebApi5.BookOperations.UpdateBook;
using WebApi5.BookOperations.DeleteBook;

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
        public IActionResult GetById(int Id) 
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context);
            query.BookId = Id;
            try
            {
                var result = query.Handle();
                return Ok(result);
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }
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
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            try
            {
                command.UpdateModel = updatedBook;
                command.BookId = id;
                command.Handle();
                return Ok();
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;
            try
            {
                command.Handle();
                return Ok();
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}