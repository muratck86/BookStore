using System;
using FluentAssertions;
using WebApi5.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi5.UnitTests.TestSetup;
using Xunit;

namespace WebApi5.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        private DeleteAuthorCommand _command = new DeleteAuthorCommand(null, null);
        private DeleteAuthorCommandValidator _validator = new DeleteAuthorCommandValidator();

        [Fact]
        public void When0IsGivenAsId_Validator_ShouldReturnError()
        {
            _command.AuthorId = 0;
            var result = _validator.Validate(_command);

            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void WhenValidNumberIsGivenAsId_Validator_ShouldNotReturnError()
        {
            _command.AuthorId = 1;

             var result = _validator.Validate(_command);

             result.Errors.Count.Should().Be(0);
        }
    }
}