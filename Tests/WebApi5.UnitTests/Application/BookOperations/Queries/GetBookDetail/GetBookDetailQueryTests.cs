using System;
using AutoMapper;
using FluentAssertions;
using WebApi5.Application.BookOperations.Queries.GetBookDetail;
using WebApi5.DbOperations;
using WebApi5.UnitTests.TestSetup;
using Xunit;

namespace WebApi5.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly GetBookDetailQuery _query;
        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
            _query = new GetBookDetailQuery(_context, _mapper);
        }
        
        [Fact]
        public void WhenNonExistingBookIdIsGiven_NullReferenceException_ShouldBeThrown()
        {
            //Arrange, we know that there are 3 books in our db, and there is no book with id = 10
            _query.BookId = 10000;

            //act & assert
            FluentActions.Invoking(() => _query.Handle()).Should().Throw<NullReferenceException>();
        }

        [Fact]
        public void WhenExistingBookIdIsGiven_Book_ShouldBeReturned()
        {
            //Arrange, we know that there are 3 books in our db with ids = 1, 2, 3
            _query.BookId = 1;

            //act & assert
            var result = _query.Handle();
            Assert.NotNull(result);
            Assert.IsType<BookDetailViewModel>(result);
        }
    }
}