using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ClassSchedule_TQEMPSetting : Common.Entity
    {
        public int ClassScheduleId { get; set; }
        public bool TQRequired { get; set; }

        public bool ReleaseOnClassStart { get; set; }

        public bool ReleaseOnClassEnd { get; set; }

        public int? SpecificTime { get; set; }

        public bool PriorToSpecificTime { get; set; }
        public bool ShowTaskSuggestions { get; set; }
        public bool ShowTaskQuestions { get; set; }

        public virtual ClassSchedule ClassSchedule { get; set; }
        public ClassSchedule_TQEMPSetting() { }

        public ClassSchedule_TQEMPSetting(int classScheduleId,bool tqRequired,bool releaseOnClassStart,bool releaseOnClassEnd,int? specificTime,bool priorToSpecificTime, bool showTaskSuggestions, bool showTaskQuestions)
        {
            ClassScheduleId = classScheduleId;
            TQRequired = tqRequired;
            ReleaseOnClassStart = releaseOnClassStart;
            ReleaseOnClassEnd = releaseOnClassEnd;
            SpecificTime = specificTime;
            PriorToSpecificTime = priorToSpecificTime;
            ShowTaskSuggestions = showTaskSuggestions;
            ShowTaskQuestions = showTaskQuestions;
        }

    }
}
