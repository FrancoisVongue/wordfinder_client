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
        
        
        [Required]
        [MinLength(2, ErrorMessage = "Login can not be less than 2 characters")]
        [StringLength(50, ErrorMessage = "Login can not exceed 50 characters")]
        public string Login { get; set; }
        
        
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        
        
        [Required]
        [MinLength(5, ErrorMessage = "Password can not be less than 5 characters")]
        [StringLength(90, ErrorMessage = "Password can not be longer than 90 characters")]
        public string Password { get; set; }
        
        
        public string Salt { get; set; }
        
        
        
        public IEnumerable<Topic> Topics { get; set; }
        
        
        public IEnumerable<Tag> Tags { get; set; }
        
        
        public IEnumerable<Word> Words { get; set; }
    }
}