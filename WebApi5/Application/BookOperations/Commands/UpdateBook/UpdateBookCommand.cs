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
                
            book.Title = UpdateModel.Title;
            book.PageCount = UpdateModel.PageCount;
            book.PublishDate = UpdateModel.PublishDate;
            book.GenreId = UpdateModel.GenreId;

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