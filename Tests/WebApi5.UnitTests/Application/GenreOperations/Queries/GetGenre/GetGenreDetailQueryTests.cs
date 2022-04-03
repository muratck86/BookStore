using System;
using AutoMapper;
using FluentAssertions;
using WebApi5.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi5.DbOperations;
using WebApi5.UnitTests.TestSetup;
using Xunit;

namespace WebApi5.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly GetGenreDetailQuery _query;
        public GetGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
            _query = new GetGenreDetailQuery(_context, _mapper);
        }
        
        [Fact]
        public void WhenNonExistingGenreIdIsGiven_NullReferenceException_ShouldBeThrown()
        {
            _query.GenreId = 100000;

            //act & assert
            FluentActions.Invoking(() => _query.Handle()).Should().Throw<NullReferenceException>();
        }

        [Fact]
        public void WhenExistingGenreIdIsGiven_Genre_ShouldBeReturned()
        {
            //Arrange, we know that there are 3 genres in our db with ids = 1, 2, 3
            _query.GenreId = 1;

            //act & assert
            var result = _query.Handle();
            Assert.NotNull(result);
            Assert.IsType<GenreDetailViewModel>(result);
        }
    }
}