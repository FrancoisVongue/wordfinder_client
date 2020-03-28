using System.ComponentModel.DataAnnotations.Schema;

namespace WordFinder_Domain.Models
{
    [Table(name:"Translations")]
    public class Translation
    {
        public long Id { get; set; }
        public long WordId { get; set; }
        public string Content { get; set; }
        public int Order { get; set; }

        public Word Word { get; set; }
    }
}