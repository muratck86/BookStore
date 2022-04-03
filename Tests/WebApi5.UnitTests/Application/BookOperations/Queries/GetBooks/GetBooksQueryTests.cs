using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi5.Application.BookOperations.Queries.GetBooks;
using WebApi5.DbOperations;
using WebApi5.UnitTests.TestSetup;
using Xunit;

namespace WebApi5.UnitTests.Application.BookOperations.Queries.GetBooks
{
    public class GetBooksQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly GetBooksQuery _query;
        public GetBooksQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
            _query = new GetBooksQuery(_context, _mapper);
        }
        
        [Fact]
        public void WhenCalled_ListOfBooks_ShouldBeReturned()
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