using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Procedure_ILA_Link : Entity
    {
        public int ProcedureId { get; set; }

        public int ILAId { get; set; }

        public virtual Procedure Procedure { get; set; }

        public virtual ILA ILA { get; set; }

        public Procedure_ILA_Link(Procedure procedure, ILA iLA)
        {
            ProcedureId = procedure.Id;
            ILAId = iLA.Id;
            Procedure = procedure;
            ILA = iLA;
        }

        public Procedure_ILA_Link()
        {
        }
    }
}
