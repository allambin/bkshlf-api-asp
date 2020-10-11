using AutoMapper;
using BKSHLF.Models;

namespace BKSHLF.Profiles
{
    public class BooksProfile : Profile
    {
        public BooksProfile()
        {
            CreateMap<Book, Dto.Book>();
        }
    }
}