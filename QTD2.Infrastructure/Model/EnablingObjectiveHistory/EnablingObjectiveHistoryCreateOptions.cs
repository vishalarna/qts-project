using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.EnablingObjectiveHistory
{
    public class EnablingObjectiveHistoryCreateOptions
    {
        public int EnablingObjectiveId { get; set; }

        public int Version_EnablingObjectiveId { get; set; }

        public bool OldStatus { get; set; }

        public bool NewStatus { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }

        public string ChangeNotes { get; set; }
    }
}
