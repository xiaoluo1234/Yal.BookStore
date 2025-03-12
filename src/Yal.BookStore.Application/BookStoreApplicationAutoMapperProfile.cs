using AutoMapper;
using Yal.BookStore.Authors;
using Yal.BookStore.Books;

namespace Yal.BookStore;

public class BookStoreApplicationAutoMapperProfile : Profile
{
    public BookStoreApplicationAutoMapperProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<Author, AuthorDto>();
    }
}
