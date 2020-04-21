using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NUnit.Framework;
using WordFinder_Domain.AutomapperConfig;
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
            var userProfile = new UserProfile();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(userProfile);
            });
            
            configuration.AssertConfigurationIsValid();

            var source = new User() {Words = new List<Word>()
            {
                new Word(){Content = "hello"},
                new Word(){Content = "bye"}
            }};
            
            var mapper = new Mapper(configuration);
            var dest = mapper.Map<User, UserInfo>(source);

            var words = dest.Words;
            Assert.That(words, Is.EquivalentTo(new []
            {
                "hello", 
                "bye"
            }));
        }
    }
}