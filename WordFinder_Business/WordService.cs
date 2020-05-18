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

        private const string TopicDelimiter = " >>>  <<< ";

        private ApiContext _context;

        public WordService() {}
        public WordService(ApiContext context)
        {
            _context = context;
        }

        
        public IEnumerable<Word> GetUserWords(long userId, int amount)
        {
            var userWords = getUserWords(userId)
                .OrderByDescending(w => w.AdditionTime)
                .Take(amount);
            
            return userWords;
        }

        public IEnumerable<Word> AddWords(long userId, IEnumerable<Word> words)
        {
            var allTags = new List<string>();
            foreach (var w in words)
            {
                var tags = w.WordTags.Select(wt => wt.Tag.Name);
                var topic = w.Topic.Name;
                allTags.AddRange(tags);
            }
            addTagsByName(userId, allTags);
            var tagsFromDb = getTagsByName(userId, allTags);
            var topicsFromDb = _context.Topics.ToList();
            
            foreach (var word in words)
            {
                word.UserId = userId;
                word.Topic = topicsFromDb.FirstOrDefault(t => t.Id == word.Topic?.Id);
                
                foreach (var wt in word.WordTags)
                {
                    var tagFromDb = tagsFromDb.FirstOrDefault(t => t.Name == wt.Tag.Name);
                    wt.TagId = tagFromDb.Id;
                }
            }
            _context.AddRange(words);
            _context.SaveChanges();
            return words;
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
            var words = getUserWords(userId);
            
            var selectedWords = words.ToList();
            selectedWords = WordSearchHandler.FilterByContent(selectedWords, info.Content).ToList();
            selectedWords = WordSearchHandler.FilterByTopics(selectedWords, info.TopicIds).ToList();
            selectedWords = WordSearchHandler.FilterByTags(selectedWords, info.TagIds).ToList();
            
            return selectedWords;
        }

        public IEnumerable<Word> GetWordsForRepetition(long userId, int amount)
        {
            var userWords = getUserWords(userId);
            
            var wordsToRepeat = userWords    
                .Where(w => w.Translations.Any() && !w.Familiar)
                .OrderBy(w => w.TimesRepeated)
                .Take(amount);

            return wordsToRepeat;
        }

        public IEnumerable<Word> RepeatWords(long userId, IEnumerable<long> wordsIds)
        {
            var userWords = getUserWords(userId).ToList();
            var repeatedWords = userWords.Where(w => wordsIds.Contains(w.Id));
            foreach (var word in repeatedWords)
            {
                word.TimesRepeated++;
            }

            _context.SaveChanges();
            return repeatedWords;
        }

        public Word UpdateWord(long userId, Word updatedWord)
        {
            var originalWord = getUserWords(userId)
                .FirstOrDefault(w => w.Id == updatedWord.Id);
            
            if (originalWord == null)
                throw new SecurityException("Attempt to update invalid word.");
            
            
            if (!String.IsNullOrWhiteSpace(updatedWord.Content))
                originalWord.Content = updatedWord.Content;

            UpdateTags();
            UpdateTranslations();

            void UpdateTranslations()
            {
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

            void UpdateTags()
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

        public IEnumerable<Word> FindNewWords(long userId, Topic topic)
        {
            var topicFromDb = AddTopic(userId, topic);
            var foundWords = WordSearchHandler.FindWordsInText(topic.Content);
            var userWords = getUserWords(userId).Select(w => w.Content);

            var words = foundWords
                .Except(userWords, StringComparer.OrdinalIgnoreCase)
                .Select(w => new Word() {Content = w, Topic = topicFromDb});

            return words;
        }

        private Topic AddTopic(long userId, Topic topic)
        {
            var topicFromDb = _context
                .Users
                    .Include(u => u.Topics)
                .FirstOrDefault(u => u.Id == userId)?
                .Topics?
                .FirstOrDefault(t => t.Name == topic.Name);
            
            bool exists = topicFromDb != null;

            if (exists)
                topicFromDb.Content += TopicDelimiter + topic.Content;
            else
            {
                topic.UserId = userId;
                topicFromDb = _context.Topics.Add(topic).Entity;
            }
            _context.SaveChanges();
            
            return topicFromDb;
        }

        private Tag GetTagFromDatabase(long userId, Tag tag)
        {
            var tagFromDb = _context.Tags
                .FirstOrDefault(t => t.Name == tag.Name && t.UserId == userId);
            
            if (tagFromDb == null)
            {
                tag.UserId = userId;
                tagFromDb = _context.Tags.Add(tag).Entity;
                _context.SaveChanges();
            }
            
            return tagFromDb;
        }

        private IQueryable<Tag> getUserTags(long userId)
        {
            var tags = _context.Tags.Where(t => t.UserId == userId);
            return tags;
        }

        private IQueryable<Word> getUserWords(long userId)
        {
            var userWords = _context.Words
                .Include(w => w.WordTags)
                    .ThenInclude(wt => wt.Tag)
                .Include(w => w.Translations)
                .Include(w => w.Topic)
                .Where(w => w.UserId == userId);

            return userWords;
        }

        private IEnumerable<Tag> getTagsByName(long userId, IEnumerable<string> tagNames)
        {
            var user = _context.Users
                    .Include(u => u.Tags)
                .FirstOrDefault(u => u.Id == userId);
            
            var tags = user.Tags?
                .Where(t => tagNames.Contains(t.Name)) ?? new List<Tag>();
                
            return tags;
        }

        private IEnumerable<Tag> addTagsByName(long userId, IEnumerable<string> tagNames)
        {
            var existingTags = getTagsByName(userId, tagNames)
                .Select(t => t.Name);
            
            var tagsToAdd = tagNames
                .Where(tn => !existingTags.Contains(tn))
                .Select(tn => new Tag() {Name = tn, UserId = userId});
            
            _context.AddRange(tagsToAdd);
            _context.SaveChanges();

            return tagsToAdd;
        }
    }
}