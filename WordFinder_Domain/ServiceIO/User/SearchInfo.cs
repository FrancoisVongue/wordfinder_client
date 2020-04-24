using System.Collections.Generic;

namespace WordFinder_Domain.ServiceIO
{
    public class SearchInfo
    {
        public string Content { get; set; }

        public IEnumerable<long> TagIds { get; set; }

        public IEnumerable<long> TopicIds { get; set; }
    }
}