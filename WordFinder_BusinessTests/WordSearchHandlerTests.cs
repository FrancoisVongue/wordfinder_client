using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using WordFinder_Business;
using WordFinder_Domain.Models;

namespace WordFinder_BusinessTests
{
    public class WordSearchHandlerTests
    {
        private IEnumerable<Word> _words;
        
        [SetUp]
        public void SetUp()
        {
            var kitchenTag = new Tag() {Id = 1, Name = "kitchen"};
            var homeTag = new Tag() {Id = 2, Name = "home"};
            var familyTag = new Tag() {Id = 3, Name = "family"};
            
            var KitchenTopic = new Topic() { Id = 1, Name = "The Kitchen" };
            var FamilyTopic = new Topic() { Id = 2, Name = "The Adams Family" };
            
            _words = new[]
            {
                new Word()
                {
                    Content = "knife",
                    WordTags = new []
                    {   
                        new WordTag() { TagId = kitchenTag.Id, },
                        new WordTag() { TagId = homeTag.Id }, 
                    },
                    Topic = KitchenTopic
                }, 
                new Word()
                {
                    Content = "wife",
                    WordTags = new []
                    {
                        new WordTag() { TagId = familyTag.Id, },
                        new WordTag() { TagId = homeTag.Id }, 
                    },
                    Topic = FamilyTopic
                }, 
            };
        }
        
        [TestCase("monocai", "policai", 4)]
        [TestCase("slr", "scholar", 3)]
        [TestCase("s", "s", 1)]
        public void LongestCommonSubsequence_WhenCalledWithTwoStrings_ReturnsProperNumber(string s1, string s2, int number)
        {
            var result = WordSearchHandler.LongestCommonSubsequence(s1, s2);
            Assert.That(result, Is.EqualTo(number));
        }
        
        [TestCase("knife", "knife")]
        [TestCase("if", "knife", "wife")]
        public void FilterByContent_WhenPassedSubstring_ReturnsAppropriateWords(string content, params string[] expected)
        {
            var result = WordSearchHandler
                .FilterByContent(_words, content)
                .Select(w => w.Content);
            
            Assert.That(result, Is.EquivalentTo(expected));
        }
        
        [TestCase(new string[]{"knife"}, 1)]
        [TestCase(new string[]{"wife"}, 2)]
        [TestCase(new string[]{"wife", "knife"}, 2, 1)]
        public void FilterByTopics_WhenPassedTopics_ReturnsAppropriateWords(string[] expectedContents, params long[] topicIDs)
        {
            var result = WordSearchHandler
                .FilterByTopics(_words, topicIDs)
                .Select(w => w.Content);
            
            Assert.That(result, Is.EquivalentTo(expectedContents));
        }
        
        [TestCase(new string[]{"knife"}, 1)]
        [TestCase(new string[]{"wife"}, 3)]
        [TestCase(new string[]{"wife", "knife"}, 2)]
        public void FilterByTags_WhenPassedTags_ReturnsAppropriateWords(string[] expectedContents, params long[] tagIds)
        {
            var result = WordSearchHandler
                .FilterByTags(_words, tagIds)
                .Select(w => w.Content);
            
            Assert.That(result, Is.EquivalentTo(expectedContents));
        }
        
        [TestCase("Hello, hello hello nice HeLlo", new string[] {"hello", "nice"})]
        [TestCase("", new string[0])]
        [TestCase("tomorrow, yesterday, welcome, yesterday", new string[] {"tomorrow", "yesterday", "welcome"})]
        public void FindWordsInText_WhenPassedTextContent_ReturnsUniqueWords(string input, string[] expectedWords)
        {
            var result = WordSearchHandler
                .FindWordsInText(input);
            
            Assert.That(result, Is.EquivalentTo(expectedWords));
        }
        
    }
}