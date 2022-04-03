using System;
using System.Linq;
using WebApi5.DbOperations;

namespace WebApi5.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        public UpdateBookModel UpdateModel;
        public int BookId;
        public UpdateBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book == null)
                throw new NullReferenceException("Kitap bulunamadı");
                
            book.Title = UpdateModel.Title == null || string.Empty == UpdateModel.Title.Trim() ? book.Title : UpdateModel.Title.Trim();
            book.PageCount = UpdateModel.PageCount > 0 ? UpdateModel.PageCount : book.PageCount;
            book.GenreId = UpdateModel.GenreId > 0 ? UpdateModel.GenreId : book.GenreId;
            book.PublishDate = UpdateModel.PublishDate != null ? UpdateModel.PublishDate : book.PublishDate;
            book.AuthorId = UpdateModel.AuthorId > 0 ? UpdateModel.AuthorId : book.AuthorId;

            _dbContext.SaveChanges();
        }

    }

    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}