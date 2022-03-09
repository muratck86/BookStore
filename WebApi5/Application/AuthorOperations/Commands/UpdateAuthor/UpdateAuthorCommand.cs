using System;
using System.Linq;
using WebApi5.DbOperations;

namespace WebApi5.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public UpdateAuthorModel UpdateModel;
        public int AuthorId;
        public UpdateAuthorCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author == null)
                throw new NullReferenceException("Yazar bulunamadı");
                
            author.Name = UpdateModel.Name;
            author.LastName = UpdateModel.LastName;
            author.BirthDate = UpdateModel.BirthDate;

            _dbContext.SaveChanges();
        }

    }

    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}