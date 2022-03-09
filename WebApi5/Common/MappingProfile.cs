using AutoMapper;
using WebApi5.Application.BookOperations.Commands.CreateBook;
using WebApi5.Application.BookOperations.Queries.GetBookDetail;
using WebApi5.Application.BookOperations.Queries.GetBooks;
using WebApi5.Application.BookOperations.Commands.UpdateBook;
using WebApi5.Entities;
using WebApi5.Application.GenreOperations.Queries.GetGenres;
using WebApi5.Application.GenreOperations.Queries.GetGenreDetail;

namespace WebApi5.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();

        }
    }
}