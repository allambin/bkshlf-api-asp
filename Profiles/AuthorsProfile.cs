using System.Linq;
using AutoMapper;
using BKSHLF.Models;

namespace BKSHLF.Profiles
{
    public class AuthorsProfile : Profile
    {
        public AuthorsProfile()
        {
            // source -> target
            CreateMap<Author, Dto.Author>()
                .ForMember(x => x.BooksCount, o => o.MapFrom(x => x.BooksAuthors.Count));
            CreateMap<Dto.AuthorRequest, Author>();
            CreateMap<Author, Dto.AuthorWithBooks>()
                .ForMember(x => x.Books, o => o.MapFrom(x => x.BooksAuthors.Select(ba => ba.Book).ToList()));
        }
    }
}