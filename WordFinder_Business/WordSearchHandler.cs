using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WordFinder_Domain.Models;

namespace WordFinder_Business
{
    public class WordSearchHandler
    {
        public static IEnumerable<string> FindWordsInText(string content)
        {
            if(String.IsNullOrWhiteSpace(content))
                return new List<string>();
            
            var pattern = new Regex(@"[\-a-z]{3,}", RegexOptions.IgnoreCase);
            
            MatchCollection matches = pattern.Matches(content);

            var words = matches
                .Select(match => match.Value.ToLower())
                .Distinct();

            return words;
        }
        
        public static IEnumerable<Word> FilterByContent(IEnumerable<Word> words, string content)
        {
            if (!String.IsNullOrWhiteSpace(content))
            {
                var wordsByContent = words.Where(w =>
                    LongestCommonSubsequence(w.Content, content) == content.Length);
                
                return wordsByContent;
            }
            else
            {
                return words;
            }
        }
        
        public static IEnumerable<Word> FilterByTopics(IEnumerable<Word> words, IEnumerable<long> topicIds)
        {
            if (topicIds?.Count() != 0)
                return words.Where(w =>
                    w.Topic != null && topicIds.Contains(w.Topic.Id));
            else
                return words;
        }
        
        public static IEnumerable<Word> FilterByTags(IEnumerable<Word> words, IEnumerable<long> tagIds)
        {
            if (tagIds?.Count() != 0)
                return words.Where(w =>
                    tagIds.Intersect(w.WordTags?.Select(wt => wt.TagId)).Count() == tagIds.Count());
            else
                return words;
        }

        public static int LongestCommonSubsequence(string s1, string s2)
        {
            int[,] table = new int[s1.Length + 1, s2.Length + 1];

            for(int i = 0; i < s1.Length; i++)
            {
                for(int j = 0; j < s2.Length; j++)
                {
                    var match = s1[i] == s2[j] ? 1 : 0;
                    
                    table[i + 1, j + 1] = new []
                    {
                        table[i, j] + match, 
                        table[i + 1, j], 
                        table[i, j + 1]
                    }.Max();
                }
            }

            return table[s1.Length, s2.Length];
        }
    }
}

// string1
// string2