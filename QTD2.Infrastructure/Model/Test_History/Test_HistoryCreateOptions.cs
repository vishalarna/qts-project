using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.Test_History
{
    public class Test_HistoryCreateOptions
    {
        public int TestId { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
