using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ClassSchedule_TestReleaseEMPSetting_Retake_Link : Common.Entity
    {
        public int ClassSchedule_TestReleaseSettingId { get; set; }
        public int RetakeTestId { get; set; }
        public virtual Test RetakeTest { get; set; }
        public virtual ClassSchedule_TestReleaseEMPSetting ClassSchedule_TestReleaseEMPSetting { get; set; }

        public ClassSchedule_TestReleaseEMPSetting_Retake_Link() { }

        public ClassSchedule_TestReleaseEMPSetting_Retake_Link(ClassSchedule_TestReleaseEMPSetting testSettings, Test test)
        {
            ClassSchedule_TestReleaseSettingId = testSettings.Id;
            RetakeTestId = test.Id;
        }
    }
}
