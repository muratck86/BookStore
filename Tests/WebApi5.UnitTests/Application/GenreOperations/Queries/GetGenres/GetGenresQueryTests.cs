using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi5.Application.GenreOperations.Queries.GetGenres;
using WebApi5.DbOperations;
using WebApi5.UnitTests.TestSetup;
using Xunit;

namespace WebApi5.UnitTests.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly GetGenresQuery _query;
        public GetGenresQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
            _query = new GetGenresQuery(_context, _mapper);
        }
        
        [Fact]
        public void WhenCalled_ListOfGenres_ShouldBeReturned()
        {
            //Arrange
            //act 
            var result = _query.Handle();
            //assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}