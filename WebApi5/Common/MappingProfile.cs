using AutoMapper;
using WebApi5.BookOperations.CreateBook;
using WebApi5.BookOperations.GetBookDetail;
using WebApi5.BookOperations.GetBooks;
using WebApi5.BookOperations.UpdateBook;

namespace WebApi5.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
        }
    }
}