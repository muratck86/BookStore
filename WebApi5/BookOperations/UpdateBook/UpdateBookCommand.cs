using System;
using System.Linq;
using WebApi5.DbOperations;
using System.Collections.Generic;
using WebApi5.Common;

namespace WebApi5.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle(UpdateBookModel model, int Id)
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == Id);
            if (book == null)
                throw new NullReferenceException("Kitap bulunamadı");
            if (model.Title != null)
                book.Title = model.Title;
            if (model.PageCount != 0)
                book.PageCount = model.PageCount;
            if (book.PublishDate != null)
                book.PublishDate = model.PublishDate;
            if (book.GenreId != 0)
                book.GenreId = model.GenreId;

            _dbContext.SaveChanges();
        }

    }

    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}