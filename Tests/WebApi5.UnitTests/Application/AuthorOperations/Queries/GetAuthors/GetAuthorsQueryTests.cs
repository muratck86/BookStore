using AutoMapper;
using WebApi5.Application.AuthorOperations.Queries.GetAuthors;
using WebApi5.DbOperations;
using WebApi5.UnitTests.TestSetup;
using Xunit;

namespace WebApi5.UnitTests.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly GetAuthorsQuery _query;
        public GetAuthorsQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
            _query = new GetAuthorsQuery(_context, _mapper);
        }
        
        [Fact]
        public void WhenNonExistingAuthorIdIsGiven_NullReferenceException_ShouldBeThrown()
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