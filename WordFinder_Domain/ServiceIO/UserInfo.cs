using System.Collections.Generic;

namespace WordFinder_Domain.ServiceIO
{
    public class UserInfo
    {
        public long Id { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        
        public IEnumerable<string> Topics { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public IEnumerable<string> Words { get; set; }
    }
}