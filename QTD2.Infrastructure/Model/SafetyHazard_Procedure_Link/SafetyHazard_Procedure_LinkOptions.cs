using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SafetyHazard_Procedure_Link
{
    public class SafetyHazard_Procedure_LinkOptions
    {
        public int[] ProcedureIds { get; set; }

        public int SafetyHazardId { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
