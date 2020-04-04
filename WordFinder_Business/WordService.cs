using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WordFinder_Domain.Models;
using WordFinder_Repository;

namespace WordFinder_Business
{
    public class WordService
    {
        private const int ContextRadius = 10;

        private ApiContext _context;

        public WordService() {}
        public WordService(ApiContext context)
        {
            _context = context;
        }

        public IEnumerable<string> FindNewWords(string content, long userId)
        {
            var foundWords = FindWords(content);
            if(!foundWords.Any()) 
                return new List<string>();
            
            var userWords = _context.Words
                .Where(w => w.UserId == userId)
                .Select(w => w.Content);
            
            var words = foundWords.Except(userWords);

            return words;
        }

        public IEnumerable<Word> AddWords(Topic receivedTopic, long userId)
        {
            var topic = GetTopicWithName(receivedTopic.Name, userId);
            
            foreach (var word in receivedTopic.Words)
            {
                word.Topic = topic;
            }
            _context.Words.AddRange(receivedTopic.Words);
            _context.SaveChanges();
            
            return receivedTopic.Words;
        }

        public IEnumerable<Word> UserWords(long id)
        {
            var userWords = _context.Words
                .Where(w => w.UserId == id);
            
            return userWords.OrderBy(w => w.Content);
        }

        public IEnumerable<Word> SearchWords(IEnumerable<Word> words, long? topicId, IEnumerable<long> tagIds)
        {
            var correctWords = words.Where(word =>
            {
                var correctTopic = (topicId == null) || word.Topic?.Id == (long) topicId;
                var wordTagIds = word.WordTags.Select(wt => wt.TagId);
                var correctTag = tagIds.Intersect(wordTagIds).Any();
                return correctTag && correctTopic;
            });

            return correctWords;
        }

        public IEnumerable<Word> GetWordsForRepetition(long userId)
        {
            var intervals = new int[] { 0, 1, 3, 7, 14, 30, 90, 360 };
            var user = _context.Users.Find(userId);
            return user.Words.Where(word =>
                DateTime.Now - word.LastRepetitionTime > TimeSpan.FromDays(intervals[word.TimesRepeated]) ||
                word.TimesRepeated == 0
            );
        }

        public static string GetInContext(Word word)
        {
            var topic = word.Topic;

            int wordIndex = topic.Content.IndexOf(word.Content, StringComparison.CurrentCultureIgnoreCase);
            int topicLength = topic.Content.Length;
            int startIndex = wordIndex > ContextRadius ? wordIndex - ContextRadius : 0;
            int contextLength = ContextRadius * 2 + word.Content.Length;
            
            if (startIndex + contextLength > topicLength) 
                contextLength = topicLength - startIndex;
            string context = topic.Content.Substring(startIndex, contextLength);
            
            return context;
        }

        private IEnumerable<string> FindWords(string content)
        {
            if(content == null)
                return new List<string>();
            
            var pattern = new Regex(@"[\-a-z]{3,}", RegexOptions.IgnoreCase);
            MatchCollection matches = pattern.Matches(content);

            var words = matches
                .Select(match => match.Value.ToLower())
                .Distinct();

            return words;
        }

        private Topic GetTopicWithName(string topicName, long userId)
        {
            var topic = _context.Users.Find(userId)
                .Topics
                .FirstOrDefault(t => t.Name == topicName);
            
            if (topic == null)
            {
                topic = _context.Topics.Add(new Topic()
                {
                    Name = topicName,
                    UserId = userId
                }).Entity;
                _context.SaveChanges();
            }
            
            return topic;
        }

        public IEnumerable<long> GetUserCollection<T>(long userId) 
            where T : class, IUserCollection
        {
            var collection = _context.Set<T>()
                .Where(t => t.UserId == userId)
                .Select(t => t.Id)
                .ToList();

            return collection;
        }
    }
}