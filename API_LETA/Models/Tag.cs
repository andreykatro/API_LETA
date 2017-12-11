using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_LETA.Models
{
    public class Tag
    {
        public Tag()
        {
            TagsLinkRecords = new HashSet<TagsLinkRecord>();
        }
        public int Id { get; set; }
        public string TagName { get; set; }

        public ICollection<TagsLinkRecord> TagsLinkRecords { get; set; }
    }
}
