using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Procedure_RegRequirement_Link
{
    public class Procedure_RR_LinkOptions
    {
        public int ProcedureId { get; set; }

        public int[] RegulatoryRequirementIds { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime? EffectiveDate { get; set; }
    }
}
