using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi5.Application.BookOperations.Commands.DeleteBook;
using WebApi5.DbOperations;
using WebApi5.Entities;
using WebApi5.UnitTests.TestSetup;
using Xunit;

namespace WebApi5.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private DeleteBookCommand _command;
        private int _maxId;
        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _maxId = _context.Books.Max<Book>(a => a.Id);
            _command = new DeleteBookCommand(_context);

        }

        [Fact]
        public void WhenExistingBookIdIsGiven_Book_ShouldBeDeleted()
        {
            //arrange
            _command.BookId = _maxId;

            //act
            FluentActions.Invoking(() => _command.Handle()).Invoke();
            var result = _context.Books.SingleOrDefault(aut => aut.Id == _maxId);

            //assert
            result.Should().Be(null);
        }

        [Fact]
        public void WhenNonExistingBookIdIsGiven_Book_ShouldReturnError()
        {
            //arrange
            _command.BookId = _maxId + 10000;

            //act & assert
            FluentActions
                .Invoking(() => _command.Handle())
                .Should().Throw<NullReferenceException>();
        }
    }
}