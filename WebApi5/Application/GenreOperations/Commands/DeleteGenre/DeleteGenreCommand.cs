using System;
using System.Linq;
using WebApi5.DbOperations;
using WebApi5.Entities;

namespace WebApi5.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }
        private readonly BookStoreDbContext _context;
        public DeleteGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
            if(genre is null)
                throw new InvalidOperationException("Bu isimde bir tür bulunamadı.");
            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }
    }
}