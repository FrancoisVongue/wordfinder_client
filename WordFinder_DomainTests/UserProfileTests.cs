using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NUnit.Framework;
using WordFinder_Domain.AutomapperConfig;
using WordFinder_Domain.AutomapperConfig.Info;
using WordFinder_Domain.Models;
using WordFinder_Domain.ServiceIO;

namespace WordFinder_DomainTests
{
    public class UserProfileTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void UserProfile_ProperlyMaps_UserToUserInfo()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserProfile());
                cfg.AddProfile(new TextInfoProfile());
                cfg.AddProfile(new TagInfoProfile());
                cfg.AddProfile(new WordInfoProfile());
            });
            
            configuration.AssertConfigurationIsValid();

            var user = new User() {Words = new List<Word>()
            {
                new Word(){Content = "hello"},
                new Word(){Content = "bye"}
            }};
            
            var mapper = new Mapper(configuration);
            var dest = mapper.Map<User, UserDTO>(user);

            var words = dest.Words.Select(w => w.Content);
            Assert.That(words, Is.EquivalentTo(new []
            {
                "hello",
                "bye"
            }));
        }
    }
}