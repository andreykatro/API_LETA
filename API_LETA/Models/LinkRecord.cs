using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_LETA.Models
{
    public class LinkRecord
    {
        public LinkRecord()
        {
            TagsLinkRecords = new HashSet<TagsLinkRecord>();
        }
        public int Id { get; set; }
        public string CreateTime { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public int OriginalUrlId { get; set; }
        public int? CategoryId { get; set; }
        public int? LanguageId { get; set; }
        public int TypeId { get; set; }

        public Category Category { get; set; }
        public OriginalUrl OriginalUrl { get; set; }
        public Language Language { get; set; }
        public Models.Type Type { get; set; }
        public ICollection<TagsLinkRecord> TagsLinkRecords { get; set; }
    }
}
