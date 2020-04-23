using System.Linq;
using AutoMapper;
using WordFinder_Domain.Models;
using WordFinder_Domain.ServiceIO;

namespace WordFinder_Domain.AutomapperConfig
{
    public class WordProfile : Profile
    {
        public WordProfile()
        {
            CreateMap<Word, WordDTO>()
                .ForMember(dest => dest.Tags,
                    opt =>
                        opt.MapFrom(src => src.WordTags))
                .ReverseMap();
        }
    }
}