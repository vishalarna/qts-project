using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.ILA_TestRelease_EMPSettings
{
    public class ILATestReleaseSettings_RetakeTestCreateOptions
    {
        public int testReleaseSettingsId { get; set; }

        public int[] retakeTestIds { get; set; }
    }
}
