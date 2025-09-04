using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ITestReleaseEMPSetting_Retake_LinkService : Common.IService<TestReleaseEMPSetting_Retake_Link>
    {
        public Task<List<TestReleaseEMPSetting_Retake_Link>> GetTestReleaseEmpSettingRetakeLinksByTestIdAsync(int testId);
    }
}
