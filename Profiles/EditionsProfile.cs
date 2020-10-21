using AutoMapper;
using BKSHLF.Models;

namespace BKSHLF.Profiles
{
    public class EditionsProfile : Profile
    {
        public EditionsProfile()
        {
            // source -> target
            CreateMap<Edition, Dto.Edition>();
            CreateMap<Dto.EditionRequest, Edition>();
        }
    }
}