using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi5.Application.BookOperations.Commands.UpdateBook;
using WebApi5.DbOperations;
using WebApi5.UnitTests.TestSetup;
using Xunit;

namespace WebApi5.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private UpdateBookCommand _command;
        private int testId = 1;
        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
            _command = new UpdateBookCommand(_context);
            
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public void WhenNullOrEmptyStringIsGiven_Title_ShouldNotUpdate(string title)
        {
            var bookToUpdate = _context.Books.SingleOrDefault(a => a.Id == testId);
            var newBookInfo = new  UpdateBookModel { Title = title };
            _command.BookId = testId;
            _command.UpdateModel = newBookInfo;

            FluentActions.Invoking(() => _command.Handle()).Invoke();

            var bookUpdated = _context.Books.SingleOrDefault(b => b.Id == testId);
            bookUpdated?.Title.Should().Be(bookToUpdate?.Title);
        }
        
        [Fact]
        public void WhenValidInputIsGiven_Title_ShouldBeUpdated()
        {
            var title = "ValidTestName";
            
            var bookToUpdate = _context.Books.SingleOrDefault(a => a.Id == testId);
            var bookWithNewInfo = new  UpdateBookModel { Title = title };
            _command.BookId = testId;
            _command.UpdateModel = bookWithNewInfo;

            FluentActions.Invoking(() => _command.Handle()).Invoke();

            var bookUpdated = _context.Books.SingleOrDefault(aut => aut.Id == testId);
            bookUpdated?.Title.Should().Be(bookWithNewInfo.Title);
        }

        [Theory]
        [InlineData(null)]
        public void WhenDateIsNull_PublishDate_ShouldNotBeUpdated(DateTime publishDate)
        {
            var bookToUpdate = _context.Books.SingleOrDefault(a => a.Id == testId);
            var bookWithNewInfo = new  UpdateBookModel { PublishDate = publishDate};
            _command.BookId = testId;
            _command.UpdateModel = bookWithNewInfo;

            FluentActions.Invoking(() => _command.Handle()).Invoke();

            var bookUpdated = _context.Books.SingleOrDefault(aut => aut.Id == testId);
            bookUpdated?.PublishDate.Should().Be(bookToUpdate?.PublishDate);
        }

        [Fact]
        public void WhenValidDateIsGiven_PublishDate_ShouldBeUpdated()
        {
            DateTime publishDate = new DateTime(1888, 10, 10);
            var bookToUpdate = _context.Books.SingleOrDefault(a => a.Id == testId);
            var bookWithNewInfo = new  UpdateBookModel { PublishDate = publishDate };
            _command.BookId = testId;
            _command.UpdateModel = bookWithNewInfo;

            FluentActions.Invoking(() => _command.Handle()).Invoke();

            var bookUpdated = _context.Books.SingleOrDefault(aut => aut.Id == testId);
            bookUpdated?.PublishDate.Should().Be(bookWithNewInfo.PublishDate);
        }

        [Fact]
        public void WhenIdIsNotGiven_GenreId_ShouldNotBeUpdated()
        {
            //arrange 
            var bookToUpdate = _context.Books.SingleOrDefault(b => b.Id == testId);
            var bookWithNewInfo = new UpdateBookModel { GenreId = 0 };
            _command.BookId = testId;
            _command.UpdateModel = bookWithNewInfo;

            //act
            FluentActions.Invoking(() => _command.Handle()).Invoke();
            var bookUpdated = _context.Books.SingleOrDefault(b => b.Id == testId);

            //assert
            bookUpdated?.GenreId.Should().Be(bookToUpdate?.GenreId);
        }

        [Fact]
        public void WhenIdIsGiven_GenreId_ShouldBeUpdated()
        {
            //arrange 
            var bookToUpdate = _context.Books.SingleOrDefault(b => b.Id == testId);
            var bookWithNewInfo = new UpdateBookModel { GenreId = 1 };
            _command.BookId = testId;
            _command.UpdateModel = bookWithNewInfo;

            //act
            FluentActions.Invoking(() => _command.Handle()).Invoke();
            var bookUpdated = _context.Books.SingleOrDefault(b => b.Id == testId);

            //assert
            bookUpdated?.GenreId.Should().Be(bookWithNewInfo.GenreId);
        }
        [Fact]
        public void WhenIdIsNotGiven_PageCount_ShouldNotBeUpdated()
        {
            //arrange 
            var bookToUpdate = _context.Books.SingleOrDefault(b => b.Id == testId);
            var bookWithNewInfo = new UpdateBookModel { PageCount = 0 };
            _command.BookId = testId;
            _command.UpdateModel = bookWithNewInfo;

            //act
            FluentActions.Invoking(() => _command.Handle()).Invoke();
            var bookUpdated = _context.Books.SingleOrDefault(b => b.Id == testId);

            //assert
            bookUpdated?.PageCount.Should().Be(bookToUpdate?.PageCount);
        }

        [Fact]
        public void WhenIdIsGiven_PageCount_ShouldBeUpdated()
        {
            //arrange 
            var bookToUpdate = _context.Books.SingleOrDefault(b => b.Id == testId);
            var bookWithNewInfo = new UpdateBookModel { PageCount = 1 };
            _command.BookId = testId;
            _command.UpdateModel = bookWithNewInfo;

            //act
            FluentActions.Invoking(() => _command.Handle()).Invoke();
            var bookUpdated = _context.Books.SingleOrDefault(b => b.Id == testId);

            //assert
            bookUpdated?.PageCount.Should().Be(bookWithNewInfo.PageCount);
        }
        [Fact]
        public void WhenIdIsNotGiven_AuthorId_ShouldNotBeUpdated()
        {
            //arrange 
            var bookToUpdate = _context.Books.SingleOrDefault(b => b.Id == testId);
            var bookWithNewInfo = new UpdateBookModel { AuthorId = 0 };
            _command.BookId = testId;
            _command.UpdateModel = bookWithNewInfo;

            //act
            FluentActions.Invoking(() => _command.Handle()).Invoke();
            var bookUpdated = _context.Books.SingleOrDefault(b => b.Id == testId);

            //assert
            bookUpdated?.AuthorId.Should().Be(bookToUpdate?.AuthorId);
        }

        [Fact]
        public void WhenIdIsGiven_AuthorId_ShouldBeUpdated()
        {
            //arrange 
            var bookToUpdate = _context.Books.SingleOrDefault(b => b.Id == testId);
            var bookWithNewInfo = new UpdateBookModel { AuthorId = 1 };
            _command.BookId = testId;
            _command.UpdateModel = bookWithNewInfo;

            //act
            FluentActions.Invoking(() => _command.Handle()).Invoke();
            var bookUpdated = _context.Books.SingleOrDefault(b => b.Id == testId);

            //assert
            bookUpdated?.AuthorId.Should().Be(bookWithNewInfo.AuthorId);
        }
    }
}