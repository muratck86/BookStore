using FluentAssertions;
using WebApi5.Application.GenreOperations.Commands.DeleteGenre;
using WebApi5.UnitTests.TestSetup;
using Xunit;

namespace WebApi5.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private DeleteGenreCommand _command = new DeleteGenreCommand(null, null);
        private DeleteGenreCommandValidator _validator = new DeleteGenreCommandValidator();

        [Fact]
        public void When0IsGivenAsId_Validator_ShouldReturnError()
        {
            _command.GenreId = 0;
            var result = _validator.Validate(_command);

            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void WhenValidNumberIsGivenAsId_Validator_ShouldNotReturnError()
        {
            _command.GenreId = 1;

             var result = _validator.Validate(_command);

             result.Errors.Count.Should().Be(0);
        }
    }
}