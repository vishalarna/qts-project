using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ClassSchedule_Roster_Response_Selection : Common.Entity
    {
        public int ClassScheduleRosterResponseId { get; set; }

        public string UserAnswer { get; set; }

        public string MatchValue { get; set; }

        public int? CorrectIndex { get; set; }

        public virtual ClassSchedule_Roster_Response Response { get; set; }

        public ClassSchedule_Roster_Response_Selection()
        {

        }

        public ClassSchedule_Roster_Response_Selection(string userAnswer, string matchValue, int? correctIndex)
        {
            UserAnswer = userAnswer;
            MatchValue = matchValue;
            CorrectIndex = correctIndex;
        }
    }
}
