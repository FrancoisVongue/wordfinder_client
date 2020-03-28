using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordFinder_Domain.Models
{
    [Table("Users")]
    public class User 
    {
        public long Id { get; set; }
        
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public bool Available { get; set; }
        
        public IEnumerable<Topic> Topics { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public IEnumerable<Word> Words { get; set; }
    }
}