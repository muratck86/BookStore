using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi5.Application.GenreOperations.Commands.CreateGenre;
using WebApi5.DbOperations;
using WebApi5.UnitTests.TestSetup;
using Xunit;

namespace WebApi5.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            var genre = _context.Genres.FirstOrDefault(a => a.Id == 1);

            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = new CreateGenreModel {Name = genre?.Name};
            //Act & Assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            var newGenre = new CreateGenreModel
            {
                Name = "Test new Genre Name",
            };

            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = newGenre;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var genre = _context.Genres.SingleOrDefault(genre => genre.Name == newGenre.Name);
            genre.Should().NotBeNull();
            if (genre is not null)
                genre.Name.Should().Be(newGenre.Name);
        }

    }
}