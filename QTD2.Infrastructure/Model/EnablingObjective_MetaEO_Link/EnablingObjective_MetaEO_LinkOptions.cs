using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.EnablingObjective_MetaEO_Link
{
    public class EnablingObjective_MetaEO_LinkOptions
    {
        public int[] EOIDs { get; set; }

        public int MetaEOId { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
