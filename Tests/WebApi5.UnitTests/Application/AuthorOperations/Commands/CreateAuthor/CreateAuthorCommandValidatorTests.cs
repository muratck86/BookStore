using System;
using FluentAssertions;
using WebApi5.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi5.UnitTests.TestSetup;
using Xunit;

namespace WebApi5.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        CreateAuthorCommand command = new CreateAuthorCommand(null, null);
        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();

        [Theory]
        [InlineData("","")]
        [InlineData("TestName","")]
        [InlineData("","TestLastName")]
        [InlineData(" "," ")]
        [InlineData("TestName"," ")]
        [InlineData(" ","TestLastName")]
        [InlineData("1 ","TestLastName")]
        [InlineData("TestName ","TestLastName0")]
        [InlineData("TestName2 ","TestLastName")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string name, string lastName)
        {

            command.Model = new CreateAuthorModel {Name = name, LastName = lastName, BirthDate = DateTime.Now.Date.AddYears(-30) };

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualsNowIsGiven_Validator_ShouldReturnError()
        {
            command.Model = new CreateAuthorModel 
            {
                Name = "TestValidName",
                LastName = "TestValidLastName",
                BirthDate = DateTime.Now.Date
            };

            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(1);            
        }
        
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            command.Model = new CreateAuthorModel 
            {
                Name = "TestValidName",
                LastName = "TestValidLastName",
                BirthDate = DateTime.Now.Date.AddYears(-30)
            };

            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}