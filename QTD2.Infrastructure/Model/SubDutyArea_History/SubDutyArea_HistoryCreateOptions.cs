using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.SubDutyArea_History
{
    public class SubDutyArea_HistoryCreateOptions
    {
        public int SubDutyAreaId { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }

        public string ChangeNotes { get; set; }
    }
}
