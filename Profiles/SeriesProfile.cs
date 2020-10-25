using System.Linq;
using AutoMapper;
using BKSHLF.Models;

namespace BKSHLF.Profiles
{
    public class SeriesProfile : Profile
    {
        public SeriesProfile()
        {
            // source -> target
            CreateMap<Serie, Dto.Serie>()
                .ForMember(x => x.Books, o => o.MapFrom(x => x.BooksSeries.OrderBy(x => x.Order).Select(bs => bs.Book).ToList()));
            CreateMap<Dto.SerieRequest, Serie>();
        }
    }
}