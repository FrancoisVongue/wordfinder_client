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
    public class TagInfoProfileTests
    {
        private MapperConfiguration _configuration;
        private List<TagInfo> _tags;
        private List<WordTag> _wordTags;

        [SetUp]
        public void SetUp()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TagInfoProfile());
            });

            var tag1 = new TagInfo() {Name = "kitchen", Id = 1};
            var tag2 = new TagInfo() {Name = "room", Id = 2};
            var tag3 = new TagInfo() {Name = "house", Id = 3};
            
            _tags = new List<TagInfo>() { tag1, tag2, tag3 };
        }
        
        [Test]
        public void TagInfoProfile_ProperlyMaps_TagInfoIntoWordTag()
        {
            _configuration.AssertConfigurationIsValid();
            
            var tag1 = new TagInfo() {Name = "kitchen", Id = 1};
            var mapper = new Mapper(_configuration);
            var dest = mapper.Map<TagInfo, WordTag>(tag1);
            
            Assert.That(dest.Tag.Name, Is.EqualTo("kitchen"));
            Assert.That(dest.Tag.Id, Is.EqualTo(1));
        }
        
        
        [Test]
        public void TagInfoProfile_ProperlyMaps_MultipleTagInfosToWordTags()
        {
            _configuration.AssertConfigurationIsValid();
            
            var mapper = new Mapper(_configuration);
            var dest = mapper.Map<IEnumerable<TagInfo>, IEnumerable<WordTag>>(_tags);
            var destTagNames = dest.Select(wt => wt.Tag.Name);
            
            Assert.That(destTagNames, Is.EquivalentTo(new string[] {"kitchen", "room", "house"}));
        }
    }
}