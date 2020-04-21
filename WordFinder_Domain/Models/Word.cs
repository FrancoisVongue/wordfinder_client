using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordFinder_Domain.Models
{
    [Table(name:"Words")]
    public class Word
    {
        public long Id { get; set; }
        
        
        public long UserId { get; set; }
        
        
        [MinLength(2)]
        [StringLength(25)]
        public string Content { get; set; }
        

        public bool Familiar { get; set; } = false;
        
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime AdditionTime { get; set; } = DateTime.UtcNow;
        
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? LastRepetitionTime { get; set; } = DateTime.UtcNow;


        public int TimesRepeated { get; set; } = 0;
        
        
        public Topic Topic { get; set; }
        
        
        public User User { get; set; }
        
        
        public IEnumerable<WordTag> WordTags { get; set; }
        
        
        public IEnumerable<Translation> Translations { get; set; }
    }
}