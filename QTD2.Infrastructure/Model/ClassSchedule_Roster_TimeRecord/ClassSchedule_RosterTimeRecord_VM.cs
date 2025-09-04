using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule_Roster_TimeRecord
{
    public class ClassSchedule_RosterTimeRecord_VM
    {
        public int ClassSchedule_RosterId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public ClassSchedule_RosterTimeRecord_VM() { }

        public ClassSchedule_RosterTimeRecord_VM(int classSchedule_RosterId, DateTime startDateTime, DateTime endDateTime) 
        {
            ClassSchedule_RosterId = classSchedule_RosterId;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
        }
    }
}
