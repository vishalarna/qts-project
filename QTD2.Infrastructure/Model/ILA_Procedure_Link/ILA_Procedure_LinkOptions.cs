using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA_Procedure_Link
{
    public class ILA_Procedure_LinkOptions
    {
        public int ILAId { get; set; }

        public int[] ProcedureIds { get; set; }
    }
}
