using System;
using System.Linq;
using AutoMapper;
using WebApi5.DbOperations;
using WebApi5.Entities;

namespace WebApi5.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreModel Model { get; set; }
        private readonly BookStoreDbContext _context;
        public UpdateGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
            if(genre is null)
                throw new InvalidOperationException("Kitap türü bulunamadı");
            var isAny = _context.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower());
            if(isAny)
                throw new InvalidOperationException("Bu isimde bir tür zaten mevcut.");
            
            genre.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;
            genre.IsActive = Model.IsActive;
            _context.SaveChanges();
        }
    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}