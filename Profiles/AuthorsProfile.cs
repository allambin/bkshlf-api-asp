using AutoMapper;
using BKSHLF.Models;

namespace BKSHLF.Profiles
{
    public class AuthorsProfile : Profile
    {
        public AuthorsProfile()
        {
            // source -> target
            CreateMap<Author, Dto.Author>();
            CreateMap<Dto.AuthorRequest, Author>();
        }
    }
}