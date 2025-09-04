using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ClassSchedule_TestRelease_EmpSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IClassSchedule_TestReleaseEMPSettingsService
    {
        public Task<ClassSchedule_TestReleaseEMPSettingVM> GetAsync(int id);

        public Task<ClassSchedule_TestReleaseEMPSettingVM> CreateAsync(int classScheduleId);

        public Task<ClassSchedule_TestReleaseEMPSettingVM> UpdateAsync(int classId, ClassScheduleTestReleaseEmpSettingsCreateOptions options);
    }
}
