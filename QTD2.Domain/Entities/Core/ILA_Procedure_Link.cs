using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class ILA_Procedure_Link : Entity
    {
        public int ILAId { get; set; }

        public int ProcedureId { get; set; }

        public virtual ILA ILA { get; set; }

        public virtual Procedure Procedure { get; set; }

        public ILA_Procedure_Link()
        {
        }

        public ILA_Procedure_Link(ILA iLA, Procedure procedure)
        {
            ILAId = iLA.Id;
            ProcedureId = procedure.Id;
            ILA = iLA;
            Procedure = procedure;
        }
    }
}
