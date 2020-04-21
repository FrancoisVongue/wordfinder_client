using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace WordFinder_Domain.Models
{
    [Table("Tags")]
    public class Tag 
    {
        public long Id { get; set; }
        
        [Required]
        public long UserId { get; set; }
        
        [Required]
        [MinLength(2, ErrorMessage = "Tag Name can not be less than 2 characters")]
        [StringLength(20, ErrorMessage = "Tag Name can not exceed 20 characters")]
        public string Name { get; set; }
        
        
        public User User { get; set; }
        
        public IEnumerable<WordTag> WordTags { get; set; }
    }
}