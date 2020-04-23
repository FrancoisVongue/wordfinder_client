using System.Collections.Generic;

namespace WordFinder_Domain.ServiceIO
{
    public class UserDTO
    {
        public long Id { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        
        public IEnumerable<TextInfo> Topics { get; set; }
        public IEnumerable<TagInfo> Tags { get; set; }
        public IEnumerable<WordInfo> Words { get; set; }
    }
}