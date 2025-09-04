using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class StudentEvaluationService : Common.Service<StudentEvaluation>, IStudentEvaluationService
    {
        public StudentEvaluationService(IStudentEvaluationRepository repository, IStudentEvaluationValidation validation)
            : base(repository, validation)
        {
        }
        public async System.Threading.Tasks.Task<StudentEvaluation> GetStudentEvaluationByIdAsync(int studentEvaluationId)
        {
            var stdEval = await FindAsync(x => x.Id == studentEvaluationId);
            return stdEval.FirstOrDefault();
        }

        public async System.Threading.Tasks.Task<List<StudentEvaluation>> GetStudentEvalutationResultsAsync(List<int> ilasIds, List<int> studentEvalIds, List<DateTime> dateRange)
        {
            List<Expression<Func<StudentEvaluation, bool>>> predicates = new List<Expression<Func<StudentEvaluation, bool>>>();

            if (studentEvalIds != null && studentEvalIds.Count()>0)
            {
                predicates.Add(r => studentEvalIds.Contains(r.Id));
            }

            if (ilasIds != null && ilasIds.Count()>0)
            {
                predicates.Add(r => r.ILA_StudentEvaluation_Links.Any(link => ilasIds.Contains(link.ILAId)));
            }

            var studentEvals = await FindWithIncludeAsync(predicates, new string[]
            {
                "StudentEvaluationQuestions.QuestionBank",
                "RatingScaleN.RatingScaleExpanded",
                "StudentEvaluationWithoutEmps.RatingScaleExpanded",
                "ILA_StudentEvaluation_Links" 
            });

            return studentEvals.ToList();
        }


        public async Task<StudentEvaluation> GetWithRatingScalesAsync(int evaluationId)
        {
            var studentEvals = await FindWithIncludeAsync(r => r.Id == evaluationId, new string[] { "RatingScaleN.RatingScaleExpanded" });
            return studentEvals.First();
        }

        public async Task<List<StudentEvaluation>> GetAllStudentEvaluationsAsync()
        {
            var studentEvals = await AllWithIncludeAsync(new string[] { "ILA_StudentEvaluation_Links.ILA.Provider" });
            return studentEvals.ToList();
        }
    }
}