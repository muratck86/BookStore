using System;
using FluentAssertions;
using WebApi5.Application.BookOperations.Commands.CreateBook;
using WebApi5.UnitTests.TestSetup;
using Xunit;

namespace WebApi5.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Lord of The Rings", 0, 0, 0)]
        [InlineData("Lord of The Rings", 0, 0, 1)]
        [InlineData("Lord of The Rings", 0, 1, 0)]
        [InlineData("Lord of The Rings", 0, 1, 1)]
        [InlineData("Lord of The Rings", 1, 0, 0)]
        [InlineData("Lord of The Rings", 1, 0, 1)]
        [InlineData("Lord of The Rings", 1, 1, 0)]
        [InlineData("Lord of The Rings", -1, 0, 0)]
        [InlineData("Lord of The Rings", -1, -1, 1)]
        [InlineData("Lord of The Rings", 0, 1, -1)]
        [InlineData("Lord of The Rings", -1, 1, 1)]
        [InlineData("Lord of The Rings", 1, 0, -1)]
        [InlineData("Lord of The Rings", 1, -1, 1)]
        [InlineData("Lord of The Rings", 1, 1, -1)]
        [InlineData(" ", 5, 5, 10)]
        [InlineData("", 0, 0, 0)]
        [InlineData("", 0, 0, 1)]
        [InlineData("", 0, 1, 0)]
        [InlineData("", 0, 1, 1)]
        [InlineData("", 1, 0, 0)]
        [InlineData("", 1, 0, 1)]
        [InlineData("", 1, 1, 0)]
        [InlineData("", 1, 1, 1)]
        [InlineData("", -1, 0, 0)]
        [InlineData("", -1, -1, 1)]
        [InlineData("", 0, 1, -1)]
        [InlineData("", -1, 1, 1)]
        [InlineData("", 1, 0, -1)]
        [InlineData("", 1, -1, 1)]
        [InlineData("", 1, 1, -1)]
        [InlineData("", 1, 1, 1)]
        [InlineData("L", 0, 0, 0)]
        [InlineData("L", 0, 0, 1)]
        [InlineData("L", 0, 1, 0)]
        [InlineData("L", 0, 1, 1)]
        [InlineData("L", 1, 0, 0)]
        [InlineData("L", 1, 0, 1)]
        [InlineData("L", 1, 1, 0)]
        [InlineData("L", 1, 1, 1)]
        [InlineData("L", -1, 0, 0)]
        [InlineData("L", -1, -1, 1)]
        [InlineData("L", 0, 1, -1)]
        [InlineData("L", -1, 1, 1)]
        [InlineData("L", 1, 0, -1)]
        [InlineData("L", 1, -1, 1)]
        [InlineData("L", 1, 1, -1)]
        [InlineData("L", 1, 1, 1)]
        [InlineData("L", 0, 0, 0)]
        [InlineData("L", 0, 0, 1)]
        [InlineData("L", 0, 1, 0)]
        [InlineData("L", 0, 1, 1)]
        [InlineData("L", 1, 0, 0)]
        [InlineData("L", 1, 0, 1)]
        [InlineData("L", 1, 1, 0)]
        [InlineData("L", 1, 1, 1)]
        [InlineData("L", -1, 0, 0)]
        [InlineData("L", -1, -1, 1)]
        [InlineData("L", 0, 1, -1)]
        [InlineData("L", -1, 1, 1)]
        [InlineData("L", 1, 0, -1)]
        [InlineData("L", 1, -1, 1)]
        [InlineData("L", 1, 1, -1)]
        [InlineData("L", 1, 1, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId, int authorId)
        {
            //Arrange
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel
            {
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId = genreId,
                AuthorId = authorId
            };

            //Act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel
            {
                Title = "Test Title",
                PageCount = 200,
                PublishDate = DateTime.Now.Date,
                GenreId = 2,
                AuthorId = 2
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel
            {
                Title = "Test Title",
                PageCount = 200,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId = 2,
                AuthorId = 2
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
                                       //.Equals(0)
        }
    }
}