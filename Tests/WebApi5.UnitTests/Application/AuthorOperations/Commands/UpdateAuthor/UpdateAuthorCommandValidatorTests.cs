using System;
using FluentAssertions;
using WebApi5.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi5.UnitTests.TestSetup;
using Xunit;

namespace WebApi5.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        UpdateAuthorCommand command = new UpdateAuthorCommand(null);
        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();

        [Theory]
        [InlineData(null,null)]
        [InlineData("NewName",null)]
        [InlineData(null,"NewName")]

        public void WhenNullNameOrLastNameIsGiven_Validator_ShouldNotReturnError(string name, string lastName)
        {
            //arrange
            command.AuthorId = 1;
            command.UpdateModel = new UpdateAuthorModel {
                Name = name,
                LastName = lastName,
                BirthDate = new DateTime(1950,6,6)
            };

            //act
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }

        [Theory]
        [InlineData("    ","    ")]
        [InlineData("NewName","  ")]
        [InlineData("   ","NewName")]
        [InlineData("NewName","")]
        [InlineData("","NewName")]
        public void WhenEmptOrWhiteSpaceNameOrLastNameIsGiven_Validator_ShouldNotReturnError(string name, string lastName)
        {
            //arrange
            command.AuthorId = 1;
            command.UpdateModel = new UpdateAuthorModel {
                Name = name,
                LastName = lastName,
                BirthDate = new DateTime(1950,6,6)
            };

            //act
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }

        [Theory]
        [InlineData("   ab "," ab   ")]
        [InlineData("NewName"," ab ")]
        [InlineData(" ab  ","NewName")]

        public void WhenLessThan3LengthNameOrLastNameIsGiven_Validator_ShouldReturnError(string name, string lastName)
        {
            //arrange
            command.AuthorId = 1;
            command.UpdateModel = new UpdateAuthorModel {
                Name = name,
                LastName = lastName,
                BirthDate = new DateTime(1950,6,6)
            };

            //act
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenGreaterThan3LengthNameOrLastNameIsGiven_Validator_ShouldNotReturnError()
        {
            //arrange
            command.AuthorId = 1;
            command.UpdateModel = new UpdateAuthorModel {
                Name = "new name",
                LastName = "newLastName",
                BirthDate = new DateTime(1950,6,6)
            };

            //act
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
