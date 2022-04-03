using FluentAssertions;
using WebApi5.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi5.UnitTests.TestSetup;
using Xunit;

namespace WebApi5.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        private readonly GetGenreDetailQuery _query = new GetGenreDetailQuery(null, null);
        private readonly GetGenreDetailQueryValidator _validator = new GetGenreDetailQueryValidator();

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenIdLessThan1IsGiven_Validator_ShouldReturnError(int id)
        {
            //arrange
            _query.GenreId = id;

            //act
            var result = _validator.Validate(_query);

            //assert
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void WhenIdGreaterThan0IsGiven_Validator_ShouldNotReturnError()
        {
            //arrange
            _query.GenreId = 1;

            //act
            var result = _validator.Validate(_query);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}