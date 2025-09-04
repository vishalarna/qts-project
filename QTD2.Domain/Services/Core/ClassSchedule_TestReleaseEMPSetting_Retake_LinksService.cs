using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class ClassSchedule_TestReleaseEMPSetting_Retake_LinksService : Common.Service<ClassSchedule_TestReleaseEMPSetting_Retake_Link>, IClassSchedule_TestReleaseEMPSetting_Retake_LinksService
    {
        public ClassSchedule_TestReleaseEMPSetting_Retake_LinksService(IClassSchedule_TestReleaseEMPSetting_Retake_LinksRepository repository, IClassSchedule_TestReleaseEMPSetting_Retake_LinksValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<ClassSchedule_TestReleaseEMPSetting_Retake_Link>> GetClassScheduleTestReleaseEmpSettingRetakeLinksByTestIdAsync(int testId)
        {
            return (await FindWithIncludeAsync(x => x.RetakeTestId == testId, new[] { "ClassSchedule_TestReleaseEMPSetting.ClassSchedule" })).ToList();
        }
    }
}
