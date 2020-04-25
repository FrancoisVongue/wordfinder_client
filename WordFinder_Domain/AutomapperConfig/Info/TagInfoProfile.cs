using AutoMapper;
using WordFinder_Domain.Models;
using WordFinder_Domain.ServiceIO;

namespace WordFinder_Domain.AutomapperConfig.Info
{
    public class TagInfoProfile : Profile
    {
        public TagInfoProfile ()
        {
            CreateMap<Tag, TagInfo>()
                .ReverseMap();
            CreateMap<WordTag, TagInfo>()
                .ForMember(dest => dest.Name,
                    opt =>
                        opt.MapFrom(src => src.Tag.Name))
                .ForMember(dest => dest.Id,
                    opt =>
                        opt.MapFrom(src => src.TagId))
                .ReverseMap()
                .ForPath(dest => dest.Tag,
                    opt => opt.MapFrom(src => src));
        }
    }
}