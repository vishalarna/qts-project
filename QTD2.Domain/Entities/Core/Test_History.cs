using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Common;

namespace QTD2.Domain.Entities.Core
{
    public class Test_History : Entity
    {
        public int TestId { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime EffectiveDate { get; set; }

        public virtual Test Test { get; set; }

        public Test_History()
        {
        }

        public Test_History(int testId, string changeNotes, DateTime effectiveDate)
        {
            TestId = testId;
            ChangeNotes = changeNotes;
            EffectiveDate = effectiveDate;
        }
    }
}
