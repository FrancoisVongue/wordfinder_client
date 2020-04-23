using System;
using System.Linq;
using System.Net.Mime;
using AutoMapper;
using WordFinder_Domain.Models;
using WordFinder_Domain.ServiceIO;

namespace WordFinder_Domain.AutomapperConfig
{
    public class UserProfile : Profile
    {
        public UserProfile ()
        {
            CreateMap<User, UserDTO>();
        }
    }
}