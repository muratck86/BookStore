using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi5.DbOperations;

namespace WebApi5.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        public readonly BookStoreDbContext _context;
        public readonly IMapper _mapper;
        public GetGenresQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GenresViewModel> Handle()
        {
            var genres = _context.Genres.Where(x => x.IsActive).OrderBy(x => x.Id).ToList();
            List<GenresViewModel> result = _mapper.Map<List<GenresViewModel>>(genres);
            return result;
        }
    }

    public class GenresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}