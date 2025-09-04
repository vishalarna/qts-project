using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class Procedure_RR_Link : Common.Entity
    {
        public int ProcedureId { get; set; }

        public int RegulatoryRequirementId { get; set; }

        public virtual Procedure Procedure { get; set; }

        public virtual RegulatoryRequirement RegulatoryRequirement { get; set; }

        public Procedure_RR_Link(Procedure procedure, RegulatoryRequirement regRequirement)
        {
            ProcedureId = procedure.Id;
            RegulatoryRequirementId = regRequirement.Id;
            Procedure = procedure;
            RegulatoryRequirement = regRequirement;
        }

        public Procedure_RR_Link()
        {
        }
    }
}
