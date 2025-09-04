using QTD2.Infrastructure.Model.ClassSchedule_Roster_TimeRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IClassSchedule_Roster_TimeRecordService
    {
        Task<ClassSchedule_RosterTimeRecord_VM> CreateTimeRecordAsync(ClassSchedule_RosterTimeRecord_VM options);
    }
}
