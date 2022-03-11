using System;
using System.Linq;
using AutoMapper;
using WebApi5.Application.BookOperations.Queries.GetBooks;
using WebApi5.DbOperations;
using WebApi5.Entities;

namespace WebApi5.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int AuthorId;
        public DeleteAuthorCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.Where(author => author.Id == AuthorId).SingleOrDefault();
            nullCheck(author);
            hasBooksCheck(AuthorId);
            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
        }

        private void hasBooksCheck(int id)
        {
            GetBooksByAuthorQuery query = new GetBooksByAuthorQuery(_dbContext, _mapper);
            query.Id = id;

            var result = query.Handle();

            if (result.Count > 0)
                throw new InvalidOperationException("Yazara ait kitap varken yazar silinemez.");
        }

        private void nullCheck(Author author) 
        {
            if (author == null)
                throw new NullReferenceException("Belirtilen Yazar Zaten Yok");
        }

    }
}