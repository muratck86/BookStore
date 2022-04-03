using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi5.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi5.DbOperations;
using WebApi5.UnitTests.TestSetup;
using Xunit;

namespace WebApi5.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private UpdateAuthorCommand _command;
        private int testId = 1;
        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
            _command = new UpdateAuthorCommand(_context);
            
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public void WhenNullOrEmptyStringIsGiven_Name_ShouldNotUpdate(string name)
        {
            var lastName = "ValidTestName";
            var birthDate = new DateTime(1970,10,10);
            var authorToUpdate = _context.Authors.SingleOrDefault(a => a.Id == testId);
            var authorWithNewInfo = new  UpdateAuthorModel { Name = name, LastName = lastName};
            _command.AuthorId = testId;
            _command.UpdateModel = authorWithNewInfo;

            FluentActions.Invoking(() => _command.Handle()).Invoke();

            var authorUpdated = _context.Authors.SingleOrDefault(aut => aut.Id == testId);
            authorUpdated?.Name.Should().Be(authorToUpdate?.Name);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public void WhenNullOrEmptyStringIsGiven_LastName_ShouldNotUpdate(string lastName)
        {
            var name = "ValidTestName";
            var authorToUpdate = _context.Authors.SingleOrDefault(a => a.Id == testId);
            var authorWithNewInfo = new  UpdateAuthorModel { Name = name, LastName = lastName};
            _command.AuthorId = testId;
            _command.UpdateModel = authorWithNewInfo;

            FluentActions.Invoking(() => _command.Handle()).Invoke();

            var authorUpdated = _context.Authors.SingleOrDefault(aut => aut.Id == testId);
            authorUpdated?.LastName.Should().Be(authorToUpdate?.LastName);
        }
        
        [Fact]
        public void WhenValidInputsAreGiven_NameAndLastName_ShouldBeUpdated()
        {
            var name = "ValidTestName";
            var lastName = "ValidTestLastName";
            
            var authorToUpdate = _context.Authors.SingleOrDefault(a => a.Id == testId);
            var authorWithNewInfo = new  UpdateAuthorModel { Name = name, LastName = lastName};
            _command.AuthorId = testId;
            _command.UpdateModel = authorWithNewInfo;

            FluentActions.Invoking(() => _command.Handle()).Invoke();

            var authorUpdated = _context.Authors.SingleOrDefault(aut => aut.Id == testId);
            authorUpdated?.Name.Should().Be(authorWithNewInfo.Name);
            authorUpdated?.LastName.Should().Be(authorWithNewInfo.LastName);            
        }

        [Theory]
        [InlineData(null)]
        public void WhenDateIsNotGiven_BirthDate_ShouldNotBeUpdated(DateTime birthDate)
        {
            var authorToUpdate = _context.Authors.SingleOrDefault(a => a.Id == testId);
            var authorWithNewInfo = new  UpdateAuthorModel { BirthDate = birthDate};
            _command.AuthorId = testId;
            _command.UpdateModel = authorWithNewInfo;

            FluentActions.Invoking(() => _command.Handle()).Invoke();

            var authorUpdated = _context.Authors.SingleOrDefault(aut => aut.Id == testId);
            authorUpdated?.BirthDate.Should().Be(authorToUpdate?.BirthDate);
        }

        [Fact]
        public void WhenValidDateIsGiven_BirthDate_ShouldBeUpdated()
        {
            DateTime birthDate = new DateTime(1888, 10, 10);
            var authorToUpdate = _context.Authors.SingleOrDefault(a => a.Id == testId);
            var authorWithNewInfo = new  UpdateAuthorModel { BirthDate = birthDate};
            _command.AuthorId = testId;
            _command.UpdateModel = authorWithNewInfo;

            FluentActions.Invoking(() => _command.Handle()).Invoke();

            var authorUpdated = _context.Authors.SingleOrDefault(aut => aut.Id == testId);
            authorUpdated?.BirthDate.Should().Be(authorWithNewInfo.BirthDate);
        }
    }
}