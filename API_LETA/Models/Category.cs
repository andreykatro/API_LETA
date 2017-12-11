using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace API_LETA.Models
{
    public class Category
    {
        public Category()
        {
            LinkRecords = new HashSet<LinkRecord>();
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }

        public ICollection<LinkRecord> LinkRecords { get; set; }
    }
}
