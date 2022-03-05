using Microsoft.AspNetCore.Mvc;
using System;
using WebApi5.DbOperations;
using WebApi5.BookOperations.GetBooks;
using WebApi5.BookOperations.CreateBook;
using WebApi5.BookOperations.GetBookDetail;
using WebApi5.BookOperations.UpdateBook;
using WebApi5.BookOperations.DeleteBook;
using AutoMapper;

namespace WebApi5.Controllers 
{
    [ApiController]
    [Route("[Controller]s")]
    public class BookController : ControllerBase 
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();

            return Ok(result);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int Id) 
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
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

        [HttpPost]
        public IActionResult AddBook ([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
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