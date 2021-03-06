using Microsoft.AspNetCore.Mvc;
using WebApi5.DbOperations;
using AutoMapper;
using FluentValidation;
using WebApi5.Application.GenreOperations.Queries.GetGenres;
using WebApi5.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi5.Application.GenreOperations.Commands.CreateGenre;
using WebApi5.Application.GenreOperations.Commands.UpdateGenre;
using WebApi5.Application.GenreOperations.Commands.DeleteGenre;

namespace WebApi5.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);
 
            var res = query.Handle();
            return Ok(res);
        }

        [HttpGet("Id")]
        public IActionResult GetGenreDetail(int Id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            
            query.GenreId = Id;
            validator.ValidateAndThrow(query);
            var res = query.Handle();
            return Ok(res);
        }

        [HttpPost]
        public IActionResult CreateGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context);
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();

            command.Model = newGenre;
            
            validator.ValidateAndThrow(command);
            command.Handle();
            
            return Ok();
        }

        [HttpPut("Id")]
        public IActionResult UpdateGenre(int Id, [FromBody] UpdateGenreModel updateModel)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();

            command.GenreId = Id;
            command.Model = updateModel;
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpDelete("Id")]
                public IActionResult DeleteGenre(int Id)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context, _mapper);
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();

            command.GenreId = Id;
            validator.ValidateAndThrow(command);

            command.Handle();
            
            return Ok();
        }
    }
}