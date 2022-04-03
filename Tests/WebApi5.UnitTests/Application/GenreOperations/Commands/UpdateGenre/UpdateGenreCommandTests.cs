using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi5.Application.GenreOperations.Commands.UpdateGenre;
using WebApi5.DbOperations;
using WebApi5.UnitTests.TestSetup;
using Xunit;

namespace WebApi5.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private UpdateGenreCommand _command;
        private int testId = 1;
        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
            _command = new UpdateGenreCommand(_context);
            
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public void WhenNullOrEmptyStringIsGiven_Name_ShouldNotUpdate(string name)
        {
            var genreToUpdate = _context.Genres.SingleOrDefault(a => a.Id == testId);
            var genreWithNewInfo = new  UpdateGenreModel { Name = name };
            _command.GenreId = testId;
            _command.Model = genreWithNewInfo;

            FluentActions.Invoking(() => _command.Handle()).Invoke();

            var genreUpdated = _context.Genres.SingleOrDefault(aut => aut.Id == testId);
            genreUpdated?.Name.Should().Be(genreToUpdate?.Name);
        }

        
        [Fact]
        public void WhenValidInputIsGiven_Name_ShouldBeUpdated()
        {
            var name = "ValidTestName";
            
            var genreToUpdate = _context.Genres.SingleOrDefault(a => a.Id == testId);
            var genreWithNewInfo = new  UpdateGenreModel { Name = name };
            _command.GenreId = testId;
            _command.Model = genreWithNewInfo;

            FluentActions.Invoking(() => _command.Handle()).Invoke();

            var genreUpdated = _context.Genres.SingleOrDefault(aut => aut.Id == testId);
            genreUpdated?.Name.Should().Be(genreWithNewInfo.Name);
        }
    }
}