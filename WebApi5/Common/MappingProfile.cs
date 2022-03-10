using AutoMapper;
using WebApi5.Application.BookOperations.Commands.CreateBook;
using WebApi5.Application.BookOperations.Queries.GetBookDetail;
using WebApi5.Application.BookOperations.Queries.GetBooks;
using WebApi5.Entities;
using WebApi5.Application.GenreOperations.Queries.GetGenres;
using WebApi5.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi5.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi5.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi5.Application.AuthorOperations.Queries.GetAuthors;

namespace WebApi5.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.LastName))
                .ForMember(dest => dest.PublishDate, opt => 
                    opt.MapFrom(src => src.PublishDate.ToString("dd/MM/yyy")));

            CreateMap<Book, BooksViewModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.LastName))
                .ForMember(dest => dest.PublishDate, opt => 
                    opt.MapFrom(src => src.PublishDate.ToString("dd/MM/yyy")));


            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();

            CreateMap<CreateAuthorModel, Author>();
            CreateMap<Author, AuthorDetailViewModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name + " " + src.LastName))
                .ForMember(dest => dest.BirthDate, opt => 
                    opt.MapFrom(src => src.BirthDate.ToString("dd/MM/yyy")));
            CreateMap<Author, AuthorsViewModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name + " " + src.LastName))
                .ForMember(dest => dest.BirthDate, 
                    opt => opt.MapFrom(src => src.BirthDate.ToString("dd/MM/yyy")));

        }
    }
}