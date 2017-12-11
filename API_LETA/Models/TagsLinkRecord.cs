using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_LETA.Models
{
    public class TagsLinkRecord 
    {
      
        public int Id { get; set; }
        public int TagId { get; set; }
        public int LinkRecordId { get; set; }

        public LinkRecord LinkRecord { get; set; }
        public Tag Tag { get; set; }
    }
}
