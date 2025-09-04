using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA
{
    public class BulkEditOptions
    {
        public string actionType { get; set; }
        public string changeNotes { get; set; }
        public DateTime EffectiveDate { get; set; }
        public List<int> iLaIds { get; set; }
    }
}
