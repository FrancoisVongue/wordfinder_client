using System.ComponentModel.DataAnnotations;

namespace WordFinder_Domain.ServiceIO
{
    public class TagInfo
    {
        public long Id { get; set; }
        
        [MinLength(2, ErrorMessage = "Tag Name can not be less than 2 characters")]
        [StringLength(20, ErrorMessage = "Tag Name can not exceed 20 characters")]
        public string Name { get; set; }
    }
}