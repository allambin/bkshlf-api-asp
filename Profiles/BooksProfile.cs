using AutoMapper;
using BKSHLF.Models;

namespace BKSHLF.Profiles
{
    public class BooksProfile : Profile
    {
        public BooksProfile()
        {
            // source -> target
            CreateMap<Book, Dto.Book>();
            CreateMap<Dto.BookRequest, Book>();
        }
    }
}