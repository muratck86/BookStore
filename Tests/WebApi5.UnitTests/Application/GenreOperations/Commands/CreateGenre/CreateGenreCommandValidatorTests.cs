using System;
using FluentAssertions;
using WebApi5.Application.GenreOperations.Commands.CreateGenre;
using WebApi5.UnitTests.TestSetup;
using Xunit;

namespace WebApi5.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        CreateGenreCommand command = new CreateGenreCommand(null);
        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();

        [Theory]
        [InlineData("")]
        [InlineData("1d   ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string name)
        {
            command.Model = new CreateGenreModel {Name = name };

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            command.Model = new CreateGenreModel 
            {
                Name = "TestValidName"
            };

            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}