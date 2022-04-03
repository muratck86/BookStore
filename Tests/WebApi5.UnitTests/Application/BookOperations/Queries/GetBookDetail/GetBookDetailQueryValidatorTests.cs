using FluentAssertions;
using WebApi5.Application.BookOperations.Queries.GetBookDetail;
using WebApi5.UnitTests.TestSetup;
using Xunit;

namespace WebApi5.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly GetBookDetailQuery _query = new GetBookDetailQuery(null, null);
        private readonly GetBookDetailQueryValidator _validator = new GetBookDetailQueryValidator();

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenIdLessThan1IsGiven_Validator_ShouldReturnError(int id)
        {
            //arrange
            _query.BookId = id;

            //act
            var result = _validator.Validate(_query);

            //assert
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void WhenIdGreaterThan0IsGiven_Validator_ShouldNotReturnError()
        {
            //arrange
            _query.BookId = 1;

            //act
            var result = _validator.Validate(_query);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}