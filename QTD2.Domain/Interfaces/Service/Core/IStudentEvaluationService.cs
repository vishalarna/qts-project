using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IStudentEvaluationService : Common.IService<StudentEvaluation>
    {
        public System.Threading.Tasks.Task<StudentEvaluation> GetStudentEvaluationByIdAsync(int studentEvaluationId);
        public System.Threading.Tasks.Task<List<StudentEvaluation>> GetStudentEvalutationResultsAsync(List<int> ilasIds, List<int> studentEvalIds, List<DateTime> dateRange);
        public System.Threading.Tasks.Task<StudentEvaluation> GetWithRatingScalesAsync(int evaluationId);
        public System.Threading.Tasks.Task<List<StudentEvaluation>> GetAllStudentEvaluationsAsync();
    }
}
