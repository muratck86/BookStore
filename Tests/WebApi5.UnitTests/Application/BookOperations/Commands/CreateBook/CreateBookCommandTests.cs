using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi5.Application.BookOperations.Commands.CreateBook;
using WebApi5.DbOperations;
using WebApi5.Entities;
using WebApi5.UnitTests.TestSetup;
using Xunit;

namespace WebApi5.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            var book = new Book
            {
                Title = "WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",
                AuthorId = 1,
                GenreId = 1,
                PageCount = 200,
                PublishDate = new DateTime(2001,1,1)
            };

            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel {Title = book.Title};
            //Act & Assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut.");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            var newBook = new CreateBookModel
            {
                Title = "Test new book",
                AuthorId = 1,
                GenreId = 1,
                PageCount = 200,
                PublishDate = new DateTime(2001,1,1)
            };

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = newBook;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var book = _context.Books.SingleOrDefault(book => book.Title == newBook.Title);
            book.Should().NotBeNull();
            if (book is not null)
            {
                book.PageCount.Should().Be(newBook.PageCount);
                book.PublishDate.Should().Be(newBook.PublishDate);
                book.GenreId.Should().Be(newBook.GenreId);
                book.AuthorId.Should().Be((newBook.AuthorId));
            }
        }

    }
}