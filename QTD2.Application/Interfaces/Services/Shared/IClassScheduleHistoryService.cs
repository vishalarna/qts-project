using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ClassSchedule;
using QTD2.Infrastructure.Model.ClassScheduleHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IClassScheduleHistoryService
    {
        public Task<List<ClassScheduleHistory>> GetAsync();

        public Task<ClassScheduleHistory> CreateAsync(ClassScheduleHistoryCreateOptions options);

        public Task<List<ClassScheduleLatestActivityVM>> GetLatestActivityAsync();
    }
}
