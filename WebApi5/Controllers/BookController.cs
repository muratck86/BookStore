using Microsoft.AspNetCore.Mvc;
using WebApi5.DbOperations;
using WebApi5.Application.BookOperations.Queries.GetBooks;
using WebApi5.Application.BookOperations.Commands.CreateBook;
using WebApi5.Application.BookOperations.Queries.GetBookDetail;
using WebApi5.Application.BookOperations.Commands.UpdateBook;
using WebApi5.Application.BookOperations.Commands.DeleteBook;
using AutoMapper;
using FluentValidation;
using WebApi5.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi5.Application.AuthorOperations.Queries.GetAuthorDetail;

namespace WebApi5.Controllers
{
    [ApiController]
    [Route("[Controller]s")]
    public class BookController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(IBookStoreDbContext context, IMapper mapper)
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

            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = newBook;

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);

            checkGenreAndAuthor(newBook);

            command.Handle();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            command.UpdateModel = updatedBook;
            command.BookId = id;
            validator.ValidateAndThrow(command);

            checkGenreAndAuthor(updatedBook);
            command.Handle();
            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        private void checkGenreAndAuthor(CreateBookModel book)
        {
            GetGenreDetailQuery genreQuery = new GetGenreDetailQuery(_context, _mapper);
            genreQuery.GenreId = book.GenreId;
            GetGenreDetailQueryValidator genreValidator = new GetGenreDetailQueryValidator();
            genreValidator.ValidateAndThrow(genreQuery);
            genreQuery.Handle();

            GetAuthorDetailQuery authorQuery = new GetAuthorDetailQuery(_context, _mapper);
            authorQuery.AuthorId = book.AuthorId;
            GetAuthorDetailQueryValidator authorValidator = new GetAuthorDetailQueryValidator();
            authorValidator.ValidateAndThrow(authorQuery);
            authorQuery.Handle();
        }

        private void checkGenreAndAuthor(UpdateBookModel book)
        {
            GetGenreDetailQuery genreQuery = new GetGenreDetailQuery(_context, _mapper);
            genreQuery.GenreId = book.GenreId;
            GetGenreDetailQueryValidator genreValidator = new GetGenreDetailQueryValidator();
            genreValidator.ValidateAndThrow(genreQuery);
            genreQuery.Handle();

            GetAuthorDetailQuery authorQuery = new GetAuthorDetailQuery(_context, _mapper);
            authorQuery.AuthorId = book.AuthorId;
            GetAuthorDetailQueryValidator authorValidator = new GetAuthorDetailQueryValidator();
            authorValidator.ValidateAndThrow(authorQuery);
            authorQuery.Handle();
        }
    }
}