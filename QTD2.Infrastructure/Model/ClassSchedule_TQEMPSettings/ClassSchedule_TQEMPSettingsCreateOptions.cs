using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ClassSchedule_TQEMPSettings
{
    public class ClassSchedule_TQEMPSettingsCreateOptions
    {
        public int ClassScheduleId { get; set; }
        public bool TQRequired { get; set; }

        public bool ReleaseOnClassStart { get; set; }

        public bool ReleaseOnClassEnd { get; set; }

        public int? SpecificTime { get; set; }

        public bool PriorToSpecificTime { get; set; }
        public bool ShowTaskSuggestions { get; set; }
        public bool ShowTaskQuestions { get; set; }

        public ClassSchedule_TQEMPSettingsCreateOptions() { }

        public ClassSchedule_TQEMPSettingsCreateOptions(int classScheduleId, bool tqRequired)
        {
            ClassScheduleId = classScheduleId;
            TQRequired = tqRequired;
        }
    }
}
