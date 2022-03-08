using System;
using System.Linq;
using WebApi5.DbOperations;
using WebApi5.Common;
using AutoMapper;

namespace WebApi5.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public UpdateBookModel UpdateModel;
        public int BookId;
        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book == null)
                throw new NullReferenceException("Kitap bulunamadı");
                
            if (UpdateModel.Title != null)
                book.Title = UpdateModel.Title;
            if (UpdateModel.PageCount != 0)
                book.PageCount = UpdateModel.PageCount;
            if (book.PublishDate != null)
                book.PublishDate = UpdateModel.PublishDate;
            if (book.GenreId != 0)
                book.GenreId = UpdateModel.GenreId;

            _dbContext.SaveChanges();
        }

    }

    public class UpdateBookModel
    {
        public string Title { get; set; }
        public GenreEnum GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}