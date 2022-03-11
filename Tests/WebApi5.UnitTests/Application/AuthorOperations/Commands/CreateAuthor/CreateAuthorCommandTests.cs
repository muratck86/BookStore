using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi5.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi5.DbOperations;
using WebApi5.UnitTests.TestSetup;
using Xunit;

namespace WebApi5.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            // var author = new Author
            // {
            //     Name = "WhenAlreadyExistAuthorTitleIsGiven_InvalidOperationException_ShouldBeReturn",
            //     LastName = "Test LastName",
            //     BirthDate = new DateTime(2001,1,1)
            // };

            // _context.Authors.Add(author);
            // _context.SaveChanges();

            var author = _context.Authors.FirstOrDefault(a => a.Id == 1);

            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = new CreateAuthorModel {Name = author.Name, LastName = author.LastName};
            //Act & Assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>();
        }
        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            var newAuthor = new CreateAuthorModel
            {
                Name = "Test new Author Name",
                LastName = "Test new Author LastName",
                BirthDate = new DateTime(2001,1,1)
            };

            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = newAuthor;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var author = _context.Authors.SingleOrDefault(author => author.Name == newAuthor.Name && author.LastName == newAuthor.LastName);
            author.Should().NotBeNull();
            if (author is not null)
                author.BirthDate.Should().Be(newAuthor.BirthDate);
        }

    }
}