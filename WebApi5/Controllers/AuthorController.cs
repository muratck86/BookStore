using Microsoft.AspNetCore.Mvc;
using WebApi5.DbOperations;
using AutoMapper;
using FluentValidation;
using WebApi5.Application.AuthorOperations.Queries.GetAuthors;
using WebApi5.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi5.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi5.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi5.Application.AuthorOperations.Commands.DeleteAuthor;

namespace WebApi5.Controllers
{
    [ApiController]
    [Route("[Controller]s")]
    public class AuthorController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public AuthorController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);

            var result =  query.Handle();

            return Ok(result);
        }

        [HttpGet("id")]
        public IActionResult GetAuthorDetail(int id)
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = id;

            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            validator.ValidateAndThrow(query);

            var result = query.Handle();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateAuthor([FromBody]CreateAuthorModel newAuthor)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = newAuthor;

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();

        }

        [HttpPut("id")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updateAuthor)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();

            command.AuthorId = id;
            command.UpdateModel = updateAuthor;
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context, _mapper);
            command.AuthorId = id;
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }


    }
}