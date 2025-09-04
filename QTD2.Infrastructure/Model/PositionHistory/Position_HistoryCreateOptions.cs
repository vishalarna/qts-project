using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.PositionHistory
{
    public class Position_HistoryCreateOptions
    {
        public int PositionId { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string ChangeNotes { get; set; }

        public string ActionType { get; set; }

        public int[] taskIds { get; set; }
    }
}
