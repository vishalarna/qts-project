using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ClassScheduleHistory : Common.Entity
    {
        public int ClassScheduleID { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime ChangeEffectiveDate { get; set; }

        public virtual ClassSchedule ClassSchedule { get; set; }

        public ClassScheduleHistory()
        {

        }

        public ClassScheduleHistory(int classScheduleID, string changeNotes, DateTime changeEffectiveDate)
        {
            ClassScheduleID = classScheduleID;
            ChangeNotes = changeNotes;
            ChangeEffectiveDate = changeEffectiveDate;
        }
    }
}
