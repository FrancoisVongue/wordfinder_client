using System.Collections.Generic;

namespace WordFinder_Domain.ServiceIO
{
    public class ShallowWordsInfo
    {
        public IEnumerable<string> Words { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public IEnumerable<string> Topics { get; set; }
    }
}