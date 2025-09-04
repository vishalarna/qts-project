using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace QTD2.Domain.Services.Core
{
    public class EmpTestService : Common.Service<ClassSchedule_Roster_Response_Selection>, IEmpTestService
    {
        public EmpTestService(IEmpTestRepository repo, IEmpTestValidation validation)
            : base(repo, validation)
        {
        }
        public async Task<ClassSchedule_Roster_Response_Selection> GetEmployeeTestByIdAsync(int employeeId, int testId, int testTypeId, int questionId)
        {
            throw new System.NotImplementedException();
            //var obj = (await FindAsync(x => x.EmployeeId == employeeId && x.TestId == testId && x.TestTypeId == testTypeId && x.QuestionId == questionId)).FirstOrDefault();
            //return obj;
        }

       public async Task<List<ClassSchedule_Roster_Response_Selection>> GetQuestionAnswerByIdAsync(int employeeId, int testId, int ClassScheduleId, int questionId)
        {
            throw new System.NotImplementedException();
            //var questionAns =await FindAsync(x => x.QuestionId == questionId && x.ClassScheduleId == ClassScheduleId && x.TestId == testId && x.EmployeeId == employeeId);
            //return questionAns.ToList();
        }
    }
}
