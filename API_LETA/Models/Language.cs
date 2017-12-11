using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_LETA.Models
{
    public class Language
    {
        public Language()
        {
            LinkRecords = new HashSet<LinkRecord>();
        }

        public int Id { get; set; }
	    public string LanguageName { get; set; }

        public ICollection<LinkRecord> LinkRecords { get; set; }
    }
}
