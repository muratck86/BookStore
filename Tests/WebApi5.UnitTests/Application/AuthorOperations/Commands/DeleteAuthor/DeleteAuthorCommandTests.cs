using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi5.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi5.DbOperations;
using WebApi5.Entities;
using WebApi5.UnitTests.TestSetup;
using Xunit;

namespace WebApi5.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private DeleteAuthorCommand _command;
        private int _maxId;
        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
            _maxId = _context.Authors.Max<Author>(a => a.Id);
            _command = new DeleteAuthorCommand(_context, _mapper);

        }

        [Fact]
        public void WhenExistingAuthorIdIsGiven_Author_ShouldBeDeleted()
        {
            _command.AuthorId = _maxId;
            FluentActions.Invoking(() => _command.Handle()).Invoke();
            var author = _context.Authors.SingleOrDefault(aut => aut.Id == _maxId);
            author.Should().Be(null);
        }

        [Fact]
        public void WhenNonExistingAuthorIdIsGiven_Author_ShouldReturnError()
        {
            _command.AuthorId = _maxId + 1;

            FluentActions
                .Invoking(() => _command.Handle())
                .Should().Throw<NullReferenceException>();
        }

        [Fact]
        public void WhenIdOfAuhtorWithExistingBookIsGiven_Author_ShouldReturnError()
        {
            _command.AuthorId = 1;

            FluentActions
                .Invoking(() => _command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazara ait kitap varken yazar silinemez.");
        }
    }
}