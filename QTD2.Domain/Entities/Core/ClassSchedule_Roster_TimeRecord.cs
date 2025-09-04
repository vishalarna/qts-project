using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ClassSchedule_Roster_TimeRecord : Common.Entity
    {
        public int ClassSchedule_RosterId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int Sequence { get; set; }
        public ClassSchedule_Roster ClassSchedule_Roster { get; set; }

        public ClassSchedule_Roster_TimeRecord(int classSchedule_RosterId, DateTime startDateTime, DateTime endDateTime, int sequence)
        {
            ClassSchedule_RosterId = classSchedule_RosterId;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            Sequence = sequence;
        }

        public ClassSchedule_Roster_TimeRecord() { }
    }
}
