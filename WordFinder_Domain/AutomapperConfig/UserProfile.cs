using System;
using System.Linq;
using AutoMapper;
using WordFinder_Domain.Models;
using WordFinder_Domain.ServiceIO;

namespace WordFinder_Domain.AutomapperConfig
{
    public class UserProfile : Profile
    {
        public UserProfile ()
        {
            CreateMap<User, UserInfo>()
                .ForMember(dest => dest.Tags, 
                    opt 
                        => opt.MapFrom(src => src.Tags.Select(t => t.Name)))
                .ForMember(dest => dest.Topics, 
                opt 
                    => opt.MapFrom(src => src.Topics.Select(t => t.Name)))
                .ForMember(dest => dest.Words, 
                opt 
                    => opt.MapFrom(src => src.Words.Select(t => t.Content)));
            
            ShouldMapProperty = info =>
                info.Name != "Salt" &&
                info.Name != "Password";
        }
    }
}