using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Procedure_ILA_Link
{
    public class Procedure_ILA_LinkCreateOptions
    {
        public int ProcedureId { get; set; }

        public int[] ILAIds { get; set; }
    }
}
