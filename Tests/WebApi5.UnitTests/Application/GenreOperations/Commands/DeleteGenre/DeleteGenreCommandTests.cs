using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi5.Application.GenreOperations.Commands.DeleteGenre;
using WebApi5.DbOperations;
using WebApi5.Entities;
using WebApi5.UnitTests.TestSetup;
using Xunit;

namespace WebApi5.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private DeleteGenreCommand _command;
        private int _maxId;
        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
            _maxId = _context.Genres.Max<Genre>(a => a.Id);
            _command = new DeleteGenreCommand(_context, _mapper);
        }

        [Fact]
        public void WhenExistingGenreIdIsGiven_Genre_ShouldBeDeleted()
        {
            _command.GenreId = _maxId;
            FluentActions.Invoking(() => _command.Handle()).Invoke();
            var genre = _context.Genres.SingleOrDefault(aut => aut.Id == _maxId);
            genre.Should().Be(null);
        }

        [Fact]
        public void WhenNonExistingGenreIdIsGiven_Genre_ShouldReturnError()
        {
            _command.GenreId = _maxId + 10000;

            FluentActions
                .Invoking(() => _command.Handle())
                .Should().Throw<NullReferenceException>();
        }

        [Fact]
        public void WhenIdOfGenreWithExistingBookIsGiven_Genre_ShouldReturnError()
        {
            _command.GenreId = 1;

            FluentActions
                .Invoking(() => _command.Handle())
                .Should().Throw<InvalidOperationException>();
        }
    }
}