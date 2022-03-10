using System;
using System.Linq;
using WebApi5.DbOperations;

namespace WebApi5.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        public int AuthorId;
        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _dbContext = context;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.Where(author => author.Id == AuthorId).SingleOrDefault();
            if (author == null)
                throw new NullReferenceException("Belirtilen Yazar Zaten Yok");
            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
        }

    }
}