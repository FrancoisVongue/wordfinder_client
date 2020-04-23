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
    public class WordProfileTest
    {
        private MapperConfiguration _configuration;

        [SetUp]
        public void SetUp()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WordProfile());
                cfg.AddProfile(new TextInfoProfile());
                cfg.AddProfile(new TranslationInfoProfile());
                cfg.AddProfile(new TagInfoProfile());
            });
        }
        
        [Test]
        public void WordProfile_ProperlyMaps_WordToWordInfo()
        {
            _configuration.AssertConfigurationIsValid();
            
            var word = new Word()
            {
                Translations = new List<Translation>()
                {
                    new Translation() { Content = "Яблоко"},
                    new Translation() { Content = "Яблоня"},
                },
                WordTags = new List<WordTag>()
                {
                    new WordTag() { Tag = new Tag() { Name = "Cooking"}},
                    new WordTag() { Tag = new Tag() { Name = "Family"}}
                }
            };

            var mapper = new Mapper(_configuration);
            var dest = mapper.Map<Word, WordDTO>(word);
            var tags = dest.Tags.Select(t => t.Name);
            var translations = dest.Translations.Select(t => t.Content);
            
            Assert.That(tags, Is.EquivalentTo(new []
            {
                "Cooking", 
                "Family"
            }) );
            
            Assert.That(translations, Is.EquivalentTo(new []
            {
                "Яблоко",
                "Яблоня"
            }));
        }
        
        [Test]
        public void WordProfile_ProperlyMaps_WordInfoToWord()
        {
            _configuration.AssertConfigurationIsValid();
            
            var word = new WordDTO()
            {
                Translations = new List<TranslationInfo>()
                {
                    new TranslationInfo() { Content = "Яблоко"},
                    new TranslationInfo() { Content = "Яблоня"},
                },
                Tags = new List<TagInfo>()
                {
                    new TagInfo() {  Name = "Cooking", Id = 2 },
                    new TagInfo() {  Name = "Family", Id = 1 }
                }
            };

            var mapper = new Mapper(_configuration);
            var dest = mapper.Map<WordDTO, Word>(word);
            var tags = dest.WordTags.Select(wt => wt.Tag.Name);
            var tagIds = dest.WordTags.Select(wt => wt.TagId);
            var translations = dest.Translations.Select(t => t.Content);
            
            Assert.That(tags, Is.EquivalentTo(new []
            {
                "Cooking", 
                "Family"
            }) );
            
            Assert.That(translations, Is.EquivalentTo(new []
            {
                "Яблоко",
                "Яблоня"
            }));
            
            Assert.That(tagIds, Is.EquivalentTo(new [] { 1, 2 }));
        }
    }
}