using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.EnablingObjective
{
    public class EO_LinkOptions
    {
        public int EOId { get; set; }

        public int[] TaskIds { get; set; }

        public int[] ProcedureIds { get; set; }

        public int[] SafetyHazardIds { get; set; }

        public int[] RRIds { get; set; }

        public int[] IlaIds { get; set; }

        public int[] PositionIds { get; set; }

        public int[] EmployeeIds { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string ChangeNotes { get; set; }
    }
}
