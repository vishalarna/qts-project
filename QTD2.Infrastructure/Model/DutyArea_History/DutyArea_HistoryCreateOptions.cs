using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.DutyArea_History
{
    public class DutyArea_HistoryCreateOptions
    {
        public int DutyAreaId { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }

        public string ChangeNotes { get; set; }

        public int[] DutyAreaIds { get; set; }
    }
}
