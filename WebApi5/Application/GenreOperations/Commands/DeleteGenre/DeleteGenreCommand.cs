using System;
using System.Linq;
using AutoMapper;
using WebApi5.Application.BookOperations.Queries.GetBooks;
using WebApi5.DbOperations;
using WebApi5.Entities;

namespace WebApi5.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public DeleteGenreCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
            nullCheck(genre);
            hasBooksCheck(GenreId);
            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }
        private void hasBooksCheck(int id)
        {
            GetBooksByGenreQuery query = new GetBooksByGenreQuery(_context, _mapper);
            query.Id = id;

            var result = query.Handle();

            if (result.Count > 0)
                throw new InvalidOperationException("Türe ait kitap varken tür silinemez.");
        }

        private void nullCheck(Genre genre) 
        {
            if (genre == null)
                throw new NullReferenceException("Belirtilen tür zaten yok.");
        }
    }
}