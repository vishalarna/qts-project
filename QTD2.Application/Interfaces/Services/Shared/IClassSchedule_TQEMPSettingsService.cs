using QTD2.Infrastructure.Model.ClassSchedule_TQEMPSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IClassSchedule_TQEMPSettingsService
    {
        public Task<ClassSchedule_TQEMPSettingsVM> GetAsync(int id);
        public Task<ClassSchedule_TQEMPSettingsVM> CreateAsync(int classScheduleId);
        public Task<ClassSchedule_TQEMPSettingsVM> UpdateAsync(int classScheduleId, ClassSchedule_TQEMPSettingsCreateOptions options);
    }
}
