using System;
using AutoMapper;
using FluentAssertions;
using WebApi5.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi5.DbOperations;
using WebApi5.UnitTests.TestSetup;
using Xunit;

namespace WebApi5.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly GetAuthorDetailQuery _query;
        public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
            _query = new GetAuthorDetailQuery(_context, _mapper);
        }
        
        [Fact]
        public void WhenNonExistingAuthorIdIsGiven_NullReferenceException_ShouldBeThrown()
        {
            //Arrange, we know that there are 3 authors in our db, and there is no author with id = 10
            _query.AuthorId = 100000;

            //act & assert
            FluentActions.Invoking(() => _query.Handle()).Should().Throw<NullReferenceException>();
        }

        [Fact]
        public void WhenExistingAuthorIdIsGiven_Author_ShouldBeReturned()
        {
            //Arrange, we know that there are 3 authors in our db with ids = 1, 2, 3
            _query.AuthorId = 1;

            //act & assert
            var result = _query.Handle();
            Assert.NotNull(result);
            Assert.IsType<AuthorDetailViewModel>(result);
        }
    }
}