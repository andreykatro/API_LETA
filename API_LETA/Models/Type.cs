using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_LETA.Models
{
    public class Type
    {
        public Type()
        {
            LinkRecords = new HashSet<LinkRecord>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; }

        public ICollection<LinkRecord> LinkRecords { get; set; }
    }
}
