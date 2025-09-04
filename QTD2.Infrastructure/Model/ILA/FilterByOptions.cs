using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA
{
    public class FilterByOptions
    {
        public string Filter { get; set; }

        public string DoInclude { get; set; }

        public bool ActiveStatus { get; set; }

        public bool ActiveILAStatus { get; set; }

        public List<int> ProviderIds { get; set; } = new List<int>();
    }
}
