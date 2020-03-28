using System.ComponentModel.DataAnnotations.Schema;

namespace WordFinder_Domain.Models
{
    [Table("WordTags")]
    public class WordTag
    {
        public long WordId { get; set; }
        public long TagId { get; set; }
        
        public Word Word { get; set; }
        public Tag Tag { get; set; }
    }
}