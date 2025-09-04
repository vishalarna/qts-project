using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
   public class DIFSurvey_EmployeeService : Common.Service<DIFSurvey_Employee>, IDIFSurvey_EmployeeService
    {
        public DIFSurvey_EmployeeService(IDIFSurvey_EmployeeRepository repository, IDIFSurvey_EmployeeValidation validation)
            : base(repository, validation)
        {
        }
        public async Task<List<DIFSurvey_Employee>> GetCompleteDifSurveyByEmployeeId(int employeeId)
        {
            List<Expression<Func<DIFSurvey_Employee, bool>>> predicates = new List<Expression<Func<DIFSurvey_Employee, bool>>>();
            predicates.Add(x => x.EmployeeId == employeeId && x.StatusId==3);
            predicates.Add(x => x.DIFSurvey.ReleasedToEMP == true && x.DIFSurvey.Active == true);
            var difSurveyEmployees = await FindWithIncludeAsync(predicates, new[] { "DIFSurvey", "Status" });
            return difSurveyEmployees.ToList();
        }

        public async Task<List<DIFSurvey_Employee>> GetPendingDifSurveyByEmployeeId(int employeeId)
        {
            List<Expression<Func<DIFSurvey_Employee, bool>>> predicates = new List<Expression<Func<DIFSurvey_Employee, bool>>>();
            predicates.Add(x => (x.StatusId == 1 || x.StatusId == 2) && x.EmployeeId == employeeId);
            predicates.Add(x => x.DIFSurvey.ReleasedToEMP == true && x.DIFSurvey.Active == true);
            var difSurveyEmployees = await FindWithIncludeAsync(predicates, new[] { "DIFSurvey", "Status" });
            return difSurveyEmployees.ToList();
        }

        public async Task<List<DIFSurvey_Employee>> GetDifSurveyEmployeesBySurveyIdAsync(int difSurveyId)
        {
            var difSurveyTasks = await FindWithIncludeAsync(x => x.DIFSurveyId == difSurveyId, new string[] { "Employee.EmployeePositions.Position", "Employee.EmployeeOrganizations.Organization", "Employee.Person", "Status" });
            return difSurveyTasks.ToList();
        }
    }
}
