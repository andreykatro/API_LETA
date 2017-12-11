using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_LETA.Models
{
    public class OriginalUrl
    {
        public OriginalUrl()
        {
            LinkRecords = new HashSet<LinkRecord>();
        }
        public int Id { get; set; }
        public string OriginalUrlValue { get; set; }

        public ICollection<LinkRecord> LinkRecords { get; set; }
    }
}
