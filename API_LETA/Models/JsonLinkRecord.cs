using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_LETA.Models
{
    public class JsonLinkRecord
    {
       //public int Id { get; set; }
        //public string CreateTime { get; set; }
        public string Url { get; set; }
        public string OriginalUrl { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public string Language { get; set; }
        public string Category { get; set; }
        public List<string> Tags { get; set; }
        public string Type { get; set; }
    }
}
