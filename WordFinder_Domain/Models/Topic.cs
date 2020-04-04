using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordFinder_Domain.Models
{
    [Table(name:"Topics")]
    public class Topic : IUserCollection
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

        public User User { get; set; }
        public IEnumerable<Word> Words { get; set; }
    }
}