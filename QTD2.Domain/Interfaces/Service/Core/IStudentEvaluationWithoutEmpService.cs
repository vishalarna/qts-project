using QTD2.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IStudentEvaluationWithoutEmpService : Common.IService<StudentEvaluationWithoutEmp>
    {
        public System.Threading.Tasks.Task<List<StudentEvaluationWithoutEmp>> GetStudentEvaluationWithoutEmpByClassandEvalId(List<int> classscheduleIds, List<int> studentEvaluationIds);
    }
}

