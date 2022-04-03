using FluentAssertions;
using WebApi5.Application.BookOperations.Commands.DeleteBook;
using WebApi5.UnitTests.TestSetup;
using Xunit;

namespace WebApi5.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private DeleteBookCommand _command = new DeleteBookCommand(null);
        private DeleteBookCommandValidator _validator = new DeleteBookCommandValidator();

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenLessThan1IsGivenAsId_Validator_ShouldReturnError(int id)
        {
            //arrange
            _command.BookId = id;

            //act
            var result = _validator.Validate(_command);

            //assert
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void WhenValidNumberIsGivenAsId_Validator_ShouldNotReturnError()
        {
            _command.BookId = 1;

             var result = _validator.Validate(_command);

             result.Errors.Count.Should().Be(0);
        }
    }
}