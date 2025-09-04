using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.EnablingObjective_CategoryHistory
{
    public class EnablingObjective_CategoryHistoryCreateOptions
    {
        public int EnablingObjectiveCategoryId { get; set; }

        public bool OldStatus { get; set; }

        public bool NewStatus { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }

        public string ChangeNotes { get; set; }
    }
}
