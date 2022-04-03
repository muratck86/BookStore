using System;
using FluentAssertions;
using WebApi5.Application.BookOperations.Commands.UpdateBook;
using WebApi5.UnitTests.TestSetup;
using Xunit;

namespace WebApi5.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        UpdateBookCommand command = new UpdateBookCommand(null);
        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenIdLessThan1IsGiven_Validator_ShouldReturnError(int id)
        {
            //arrange
            command.BookId = id;
            command.UpdateModel = new UpdateBookModel();
            //act
            var result = validator.Validate(command);
            //assert
            result.Errors.Count.Should().Be(1);
        }

        [Theory]
        [InlineData("N")]
        [InlineData("  e")]
        [InlineData(" e  ")]
        public void WhenTitleLengthLessThan2IsGiven_Validator_ShouldReturnError(string title)
        {
            //arrange
            command.BookId = 1;
            command.UpdateModel = new UpdateBookModel { Title = title };
            //act
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(1);
        }

        [Theory]
        [InlineData("    ")]
        [InlineData("NewName")]
        [InlineData(null)]
        [InlineData("")]
        public void WhenNullEmptyWhiteSpaceOrValidTitleIsGiven_Validator_ShouldNotReturnError(string title)
        {
            //arrange
            command.BookId = 1;
            command.UpdateModel = new UpdateBookModel { Title = title };

            //act
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
        [Fact]
        public void WhenFutureDateIsGiven_Validator_ShouldReturnError()
        {
            //arrange
            command.BookId = 1;
            command.UpdateModel = new UpdateBookModel {PublishDate = DateTime.Now.Date.AddDays(1)};

            //act
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(1);
        }
        [Fact]
        public void WhenNegativeAuthorIdIsGiven_Validator_ShouldReturnError()
        {
            command.BookId = 1;
            command.UpdateModel = new UpdateBookModel { AuthorId = -1 };

            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(1);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void When0OrGreaterAuthorIdIsGiven_Validator_ShouldNotReturnError(int id)
        {
            command.BookId = 1;
            command.UpdateModel = new UpdateBookModel { AuthorId = id };

            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
        [Fact]
        public void WhenNegativeGenreIdIsGiven_Validator_ShouldReturnError()
        {
            command.BookId = 1;
            command.UpdateModel = new UpdateBookModel { GenreId = -1 };

            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(1);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void When0OrGreaterGenreIdIsGiven_Validator_ShouldNotReturnError(int id)
        {
            command.BookId = 1;
            command.UpdateModel = new UpdateBookModel { GenreId = id };

            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
        [Fact]
        public void WhenNegativePageCountIsGiven_Validator_ShouldReturnError()
        {
            command.BookId = 1;
            command.UpdateModel = new UpdateBookModel { PageCount = -1 };

            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(1);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void When0OrGreaterPageCountIsGiven_Validator_ShouldNotReturnError(int id)
        {
            command.BookId = 1;
            command.UpdateModel = new UpdateBookModel { PageCount = id };

            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}
