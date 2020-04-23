using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WordFinder_Domain.Models;

namespace WordFinder_Domain.ServiceIO
{
    public class WordDTO
    {
        public long Id { get; set; }
        
        
        public string Content { get; set; }
        

        public bool Familiar { get; set; } = false;
        
        
        public DateTime AdditionTime { get; set; } = DateTime.UtcNow;
        
        
        public DateTime? LastRepetitionTime { get; set; } = DateTime.UtcNow;


        public int TimesRepeated { get; set; } = 0;
        
        
        public TextInfo Topic { get; set; }
        
        
        public IEnumerable<TagInfo> Tags { get; set; }
        
        
        public IEnumerable<TranslationInfo> Translations { get; set; }
    }
}