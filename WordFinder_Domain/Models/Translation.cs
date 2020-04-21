using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordFinder_Domain.Models
{
    [Table(name:"Translations")]
    public class Translation
    {
        public long Id { get; set; }
        
        
        public long WordId { get; set; }
        
        [Required]
        [MinLength(2, ErrorMessage = "Translation Name can not be less than 2 characters")]
        [StringLength(20, ErrorMessage = "Translation Name can not exceed 20 characters")]
        public string Content { get; set; }


        public Word Word { get; set; }
    }
}