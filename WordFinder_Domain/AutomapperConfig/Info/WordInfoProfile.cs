using AutoMapper;
using WordFinder_Domain.Models;
using WordFinder_Domain.ServiceIO;

namespace WordFinder_Domain.AutomapperConfig.Info
{
    public class WordInfoProfile : Profile
    {
        public WordInfoProfile ()
        {
            CreateMap<Word, WordInfo>()
                .ReverseMap();
        }
    }
}