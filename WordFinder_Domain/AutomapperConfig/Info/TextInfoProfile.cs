using AutoMapper;
using WordFinder_Domain.Models;
using WordFinder_Domain.ServiceIO;

namespace WordFinder_Domain.AutomapperConfig.Info
{
    public class TextInfoProfile : Profile
    {
        public TextInfoProfile ()
        {
            CreateMap<Topic, TextInfo>()
                .ReverseMap();
        }
    }
}