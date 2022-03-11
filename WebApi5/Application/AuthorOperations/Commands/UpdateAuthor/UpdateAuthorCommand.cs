using System;
using System.Linq;
using WebApi5.DbOperations;

namespace WebApi5.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        public UpdateAuthorModel UpdateModel;
        public int AuthorId;
        public UpdateAuthorCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author == null)
                throw new NullReferenceException("Yazar bulunamadı");
            
            if (!(UpdateModel.Name == null || string.Empty == UpdateModel.Name.Trim()))
                author.Name = UpdateModel.Name.Trim();
            if (!(UpdateModel.LastName == null || string.Empty == UpdateModel.LastName.Trim()))
                author.LastName = UpdateModel.LastName.Trim();
            if (UpdateModel.BirthDate != null && UpdateModel.BirthDate.Year > 1)
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