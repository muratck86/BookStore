using FluentAssertions;
using WebApi5.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi5.UnitTests.TestSetup;
using Xunit;

namespace WebApi5.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly GetAuthorDetailQuery _query = new GetAuthorDetailQuery(null, null);
        private readonly GetAuthorDetailQueryValidator _validator = new GetAuthorDetailQueryValidator();

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenIdLessThan1IsGiven_Validator_ShouldReturnError(int id)
        {
            //arrange
            _query.AuthorId = id;

            //act
            var result = _validator.Validate(_query);

            //assert
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void WhenIdGreaterThan0IsGiven_Validator_ShouldNotReturnError()
        {
            //arrange
            _query.AuthorId = 1;

            //act
            var result = _validator.Validate(_query);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}