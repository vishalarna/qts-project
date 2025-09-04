using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Location_CategoryHistory
{
    public class Location_CategoryHistoryCreateOptions
    {
        public int LocCategoryId { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string CategoryNotes { get; set; }

        public string ActionType { get; set; }
    }
}
