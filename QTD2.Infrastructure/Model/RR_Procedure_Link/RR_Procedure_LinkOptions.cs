using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.RR_Procedure_Link
{
    public class RR_Procedure_LinkOptions
    {
        public int RegulatoryRequirementId { get; set; }

        public int[] ProcedureIds { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string ChangeNotes { get; set; }
    }
}
