using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace WordFinder_Domain.Models
{
    [Table("Tags")]
    public class Tag
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }

        public User User { get; set; }
        public IEnumerable<WordTag> WordTags { get; set; }
    }
}