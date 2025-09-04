using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class TestReleaseEMPSetting_Retake_Link : Common.Entity
    {
        public int TestReleaseSettingId { get; set; }

        public int RetakeTestId { get; set; }

        public virtual Test RetakeTest { get; set; }

        public virtual TestReleaseEMPSettings TestReleaseEMPSettings { get; set; }

        public TestReleaseEMPSetting_Retake_Link()
        {
        }

        public TestReleaseEMPSetting_Retake_Link(TestReleaseEMPSettings testSettings, Test test)
        {
            TestReleaseSettingId = testSettings.Id;
            RetakeTestId = test.Id;
        }
    }
}
