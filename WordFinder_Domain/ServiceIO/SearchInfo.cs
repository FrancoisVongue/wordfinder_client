using System.Collections.Generic;

namespace WordFinder_Domain.ServiceIO
{
    public class SearchInfo
    {
        public IEnumerable<long> TopicIds { get; set; }
        public IEnumerable<long> TagIds { get; set; }
    }
}