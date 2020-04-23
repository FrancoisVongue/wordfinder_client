using AutoMapper;
using WordFinder_Domain.Models;
using WordFinder_Domain.ServiceIO;

namespace WordFinder_Domain.AutomapperConfig.Info
{
    public class TranslationInfoProfile : Profile
    {
        public TranslationInfoProfile()
        {
            CreateMap<Translation, TranslationInfo>()
                .ReverseMap();
        }
    }
}