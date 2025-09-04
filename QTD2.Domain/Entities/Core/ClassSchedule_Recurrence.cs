using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ClassSchedule_Recurrence : Common.Entity
    {
        public int ClassId { get; set; }

        public string RecurrenceType { get; set; }

        public string RecurrencePattern { get; set; }

        public bool? Mon { get; set; }

        public bool? Tue { get; set; }

        public bool? Wed { get; set; }

        public bool? Thu { get; set; }

        public bool? Fri { get; set; }

        public bool? Sat { get; set; }

        public bool? Sun { get; set; }

        public DateTime RecurrenceStartDateTime { get; set; }

        public DateTime RecurrenceEndDateTime { get; set;}

        public int? DaysForWeeklyDailyOrMonthly { get; set; }

        public int? Month { get; set; }

        public int? NthDayMonthly { get; set; }

        public virtual ClassSchedule ClassSchedule { get; set; }

        public ClassSchedule_Recurrence()
        { 
        }

        public ClassSchedule_Recurrence(
            int classId,
            string recurrenceType,
            bool? mon, bool? tue, bool? wed, bool? thu, bool? fri, bool? sat, bool? sun,
            DateTime recurrenceStartDateTime, DateTime recurrenceEndDateTime,
            int? daysForWeeklyDailyOrMonthly, int? month, int? nthDayMonthly, string recurrencePattern)
        {
            ClassId = classId;
            RecurrenceType = recurrenceType;
            Mon = mon;
            Tue = tue;
            Wed = wed;
            Thu = thu;
            Fri = fri;
            Sat = sat;
            Sun = sun;
            RecurrenceStartDateTime = recurrenceStartDateTime;
            RecurrenceEndDateTime = recurrenceEndDateTime;
            DaysForWeeklyDailyOrMonthly = daysForWeeklyDailyOrMonthly;
            Month = month;
            NthDayMonthly = nthDayMonthly;
            RecurrencePattern = recurrencePattern;
        }
    }
}
