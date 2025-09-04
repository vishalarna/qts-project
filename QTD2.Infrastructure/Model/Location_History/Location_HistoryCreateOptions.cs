using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Location_History
{
    public class Location_HistoryCreateOptions
    {
        public int LocationId { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string Notes { get; set; }

        public string ActionType { get; set; }
        public int[] locationIds { get; set; }
    }
}
