using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IDIFSurvey_EmployeeService : Common.IService<DIFSurvey_Employee>
    {
        public Task<List<DIFSurvey_Employee>> GetCompleteDifSurveyByEmployeeId(int employeeId);
        public Task<List<DIFSurvey_Employee>> GetPendingDifSurveyByEmployeeId(int employeeId);
        public Task<List<DIFSurvey_Employee>> GetDifSurveyEmployeesBySurveyIdAsync(int difSurveyId);
    }
}
