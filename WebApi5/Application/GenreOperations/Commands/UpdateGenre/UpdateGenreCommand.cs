using System;
using System.Linq;
using WebApi5.DbOperations;

namespace WebApi5.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        public UpdateGenreCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
            if (genre == null)
                throw new NullReferenceException("Tür bulunamadı");
            
            if (!(Model.Name == null || string.Empty == Model.Name.Trim()))
                genre.Name = Model.Name.Trim();
            _context.SaveChanges();
        }
    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}