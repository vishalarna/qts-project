using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IIDPScheduleService : Common.IService<IDPSchedule>
    {
        System.Threading.Tasks.Task<List<IDPSchedule>> GetAllIDPAsync(DateTime startDate, DateTime endDate);
        System.Threading.Tasks.Task<List<IDPSchedule>> GetForTrainingProgramCompletionReportAsync(List<int> trainingProgramId, List<DateTime> dateRanges, bool includeInActiveIla, string activeInactiveEmployees);
        System.Threading.Tasks.Task<List<IDPSchedule>> GetIDPSchedulesForEmployeeCourseScheduleforGivenYear(List<int> employeeIds, string year, string activeInactiveAllILAs, string ilaCompletionStatus);
        System.Threading.Tasks.Task<List<IDPSchedule>> GetIDPSchedulesForEmployeeIDPCompletionStatusReportFulfillments(int year, List<int> employeeIds);
        System.Threading.Tasks.Task<IDPSchedule> GetIDPSchedulesByClassIdAndEmpIdAsync(int classId, int empId);
        System.Threading.Tasks.Task<List<IDPSchedule>> GetIDPSchedulesByClassScheduleIdAsync(int classId);
    }
}
