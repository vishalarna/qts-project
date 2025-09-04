using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Task
{
    public class MetaTaskOJTVM
    {
        public List<string> MetaConditions { get; set; } = new List<string>();
        
        public List<string> MetaCriteria { get; set; } = new List<string>();

        public List<string> MetaReferences { get; set; } = new List<string>();
    }
}
