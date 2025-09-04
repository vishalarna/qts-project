using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.StudentEvaluation;
using QTD2.Infrastructure.Model.StudentEvaluationWithEMP;
using QTD2.Infrastructure.Model.StudentEvaluationWithoutEmp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IStudentEvaluationWithoutEmpService
    {
        public System.Threading.Tasks.Task CreateAsync(StudentEvaluationWithoutEmpCreateOptions options);

        public Task<List<StudentEvaluationWithEMPVM>> GetDataForEvalWithEMPAsync(int evalId, int classId);

        public Task<List<StudentEvaluationWithEMPVM>> GetAllEvaluationDataForClass(int classId);

        public Task<List<StudentEvaluation>> GetEvaluationsForClassAsync(int classId);

        public System.Threading.Tasks.Task ReleaseOrRecallEvalAsync(EvalReleaseOptions option);
    }
}
