using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WordFinder_Domain.Models;
using WordFinder_Domain.ServiceIO;
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

        
        public IEnumerable<Word> GetUserWords(long id, int amount)
        {
            var userWords = _context.Words
                .Include(w => w.WordTags)
                    .ThenInclude(wt => wt.Tag)
                .Include(w => w.Translations)
                .Where(w => w.UserId == id)
                .OrderByDescending(w => w.AdditionTime)
                .Take(amount);
            
            return userWords;
        }

        public IEnumerable<Word> AddWords(long userId, IEnumerable<Word> words)
        {
            var addedWords = new List<Word>();
            
            foreach (var word in words)
            {
                word.UserId = userId;
                word.Topic = _context.Topics.Find(word.Topic.Id);
                
                foreach (var wt in word.WordTags)
                {
                    wt.Tag = _context.Tags.Find(wt.TagId);
                }

                var addedWord = _context.Words.Add(word).Entity;
                addedWords.Add(addedWord);
            }
            
            _context.SaveChanges();
            return addedWords;
        }
        
        public IEnumerable<Tag> AddTags(long userId, IEnumerable<Tag> tags)
        {
            var existingTags = _context.Tags
                .Select(t => t.Name)
                .ToList();
            
            var tagsToAdd = tags
                .Select(t => t.Name)
                .Except(existingTags)
                .Select(t => new Tag(){ Name = t, UserId = userId });
            
            _context.AddRange(tagsToAdd);
            _context.SaveChanges();
            
            var addedTags = _context.Tags
                .Where(t => !existingTags.Contains(t.Name))
                .ToList();
            
            return addedTags;
        }
        
        public IEnumerable<Word> SearchWords(long userId, SearchInfo info)
        {
            var words = _context.Words
                .Include(w => w.WordTags)
                .Include(w => w.Topic);

            var selectedWords = words.ToList();
            selectedWords = WordSearchHandler.FilterByContent(selectedWords, info.Content).ToList();
            selectedWords = WordSearchHandler.FilterByTopics(selectedWords, info.TopicIds).ToList();
            selectedWords = WordSearchHandler.FilterByTags(selectedWords, info.TagIds).ToList();
            
            return selectedWords;
        }

        public IEnumerable<Word> GetWordsForRepetition(long userId, int amount)
        {
            var user = _context.Users
                .Include(u => u.Words)
                    .ThenInclude(w => w.WordTags)
                        .ThenInclude(wt => wt.Tag)
                .Include(u => u.Words)
                    .ThenInclude(w => w.Translations)
                .Include(u => u.Words)
                    .ThenInclude(w => w.Topic)
                .FirstOrDefault(u => u.Id == userId);
            
            var wordsToRepeat = user.Words
                .OrderBy(w => w.TimesRepeated)
                .Take(amount);

            return wordsToRepeat;
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

        public Topic AddTopic(Topic receivedTopic, long userId)
        {
            var topic = GetTopicWithName(receivedTopic.Name, userId);
            
            foreach (var word in receivedTopic.Words)
            {
                word.Topic = topic;
            }

            _context.Words.AddRange(receivedTopic.Words);
            _context.SaveChanges();
            
            return receivedTopic;
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
            }
            
            return topic;
        }

        private User getUserById(long userId)
        {
            var user = _context.Users.Find(userId);
            return user;
        }

        public Word UpdateWord(long userId, Word updatedWord)
        {
            var originalWord = _context.Words
                .Include(w => w.WordTags)
                .Include(w => w.Topic)
                .Include(w => w.Translations)
                .FirstOrDefault(w => w.Id == updatedWord.Id && w.UserId == userId);
            if (originalWord == null)
                throw new SecurityException("Attempt to update invalid word.");
            
            
            if (!String.IsNullOrWhiteSpace(updatedWord.Content))
                originalWord.Content = updatedWord.Content;

            AddTags();
            AddTranslations();

            void AddTranslations()
            {
                // todo: make it for every single word
                if (updatedWord.Translations != null && updatedWord.Translations.Any())
                {
                    var originalTranslations = String.Join("", originalWord.Translations
                        .Select(t => t.Content));
                    var newTranslations = String.Join("", updatedWord.Translations
                        .Select(t => t.Content));
                
                    if(originalTranslations != newTranslations)
                        originalWord.Translations = updatedWord.Translations;
                }
            }

            void AddTags()
            {
                if (updatedWord.WordTags != null && updatedWord.WordTags.Any())
                {
                    var updatedWordTags = updatedWord.WordTags.Select(wt =>
                    {
                        wt.WordId = originalWord.Id;
                        wt.TagId = wt.Tag.Id;
                        wt.Tag = getUserTags(userId).FirstOrDefault(t => t.Id == wt.TagId);
                        wt.Word = originalWord;
                        return wt;
                    }).ToList();
                    
                    originalWord.WordTags = updatedWordTags;
                }
            }
            
            _context.SaveChanges();
            return originalWord;
        }

        private IQueryable<Tag> getUserTags(long userID)
        {
            var tags = _context.Tags.Where(t => t.UserId == userID);
            return tags;
        }
    }
}