using System;
using FluentAssertions;
using WebApi5.Application.GenreOperations.Commands.UpdateGenre;
using WebApi5.UnitTests.TestSetup;
using Xunit;

namespace WebApi5.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        UpdateGenreCommand command = new UpdateGenreCommand(null);
        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenIdLessThan1IsGiven_Validator_ShouldReturnError(int id)
        {
            //arrange
            command.GenreId = id;
            command.Model = new UpdateGenreModel();
            //act
            var result = validator.Validate(command);
            //assert
            result.Errors.Count.Should().Be(1);
        }

        [Theory]
        [InlineData(null)]
        public void WhenNullNameIsGiven_Validator_ShouldNotReturnError(string name)
        {
            //arrange
            command.GenreId = 1;
            command.Model = new UpdateGenreModel {
                Name = name
            };

            //act
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }

        [Theory]
        [InlineData("    ")]
        [InlineData("")]
        public void WhenEmptyOrWhiteSpaceNameIsGiven_Validator_ShouldNotReturnError(string name)
        {
            //arrange
            command.GenreId = 1;
            command.Model = new UpdateGenreModel {
                Name = name
            };

            //act
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }

        [Theory]
        [InlineData("   ab ")]
        public void WhenLessThan3LengthNameIsGiven_Validator_ShouldReturnError(string name)
        {
            //arrange
            command.GenreId = 1;
            command.Model = new UpdateGenreModel {
                Name = name
            };

            //act
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenGreaterThan3LengthNameIsGiven_Validator_ShouldNotReturnError()
        {
            //arrange
            command.GenreId = 1;
            command.Model = new UpdateGenreModel {
                Name = "new name"
            };

            //act
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
