using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordFinder_Domain.Models
{
    [Table(name:"Topics")]
    public class Topic
    {
        public long Id { get; set; }
        
        
        public long UserId { get; set; }
        
        [Required]
        [MinLength(2, ErrorMessage = "Text length can not be less than 2 characters")]
        [StringLength(50, ErrorMessage = "Text length can not be greater than 50 characters")]
        public string Name { get; set; }
        
        [Required]
        [StringLength(200_000, ErrorMessage = "Text content can not exceed 200,000 characters")]
        public string Content { get; set; }
        

        public User User { get; set; }
        
        
        public IEnumerable<Word> Words { get; set; }
    }
}