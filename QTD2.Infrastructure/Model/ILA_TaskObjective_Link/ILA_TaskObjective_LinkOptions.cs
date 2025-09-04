using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA_TaskObjective_Link
{
    public class ILA_TaskObjective_LinkOptions
    {
        public int ILAId { get; set; }

        public int[] TaskIds { get; set; }
        public bool IsIncludeProcedures { get; set; }
        public bool IsIncludeEos { get; set; }
    }
}
