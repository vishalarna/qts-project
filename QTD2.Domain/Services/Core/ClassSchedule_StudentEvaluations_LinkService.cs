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
    public class ClassSchedule_StudentEvaluations_LinkService : Common.Service<ClassSchedule_StudentEvaluations_Link>, IClassSchedule_StudentEvaluations_LinkService
    {
        public ClassSchedule_StudentEvaluations_LinkService(IClassSchedule_StudentEvaluations_LinkRepository repository, IClassSchedule_StudentEvaluations_LinkValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<ClassSchedule_StudentEvaluations_Link>> GetClassScheduleLinksByIlaIdEvalIdAsync(List<int> ilaIds, List<int> evalIds, List<DateTime> dateRange)
        {
            List<Expression<Func<ClassSchedule_StudentEvaluations_Link, bool>>> predicates = new List<Expression<Func<ClassSchedule_StudentEvaluations_Link, bool>>>();
            predicates.Add(r => ilaIds.Contains(r.ClassSchedule.ILAID.Value) && evalIds.Contains(r.StudentEvaluationId));
            if (dateRange.Any())
            {
                predicates.Add(r => r.ClassSchedule.StartDateTime > dateRange[0] && r.ClassSchedule.EndDateTime < dateRange[1]);
            }
            return (await FindWithIncludeAsync(predicates , new string[] { "ClassSchedule.ILA.Provider", "StudentEvaluation.RatingScaleN" , "StudentEvaluation.StudentEvaluationWithoutEmps.ClassSchedule.ClassSchedule_Evaluation_Rosters", "ClassSchedule.ILA.DeliveryMethod", "StudentEvaluation.StudentEvaluationQuestions.QuestionBank", "ClassSchedule.ClassSchedule_Evaluation_Rosters", "StudentEvaluation.StudentEvaluationWithoutEmps.RatingScaleExpanded" })).Distinct().ToList();
        }

        public async Task<List<ClassSchedule_StudentEvaluations_Link>> GetLinksByClassScheduleAndEvaluationIdsAsync(List<int> classScheduleIDs, List<int> studentEvalIds)
        {
            List<Expression<Func<ClassSchedule_StudentEvaluations_Link, bool>>> predicates = new List<Expression<Func<ClassSchedule_StudentEvaluations_Link, bool>>>();
            predicates.Add(r => classScheduleIDs.Contains(r.ClassScheduleId));
            predicates.Add(r => studentEvalIds.Contains(r.StudentEvaluationId));
            var evaluationLinks = await FindWithIncludeAsync(predicates, new string[] { "StudentEvaluation.RatingScaleN", "ClassSchedule.ILA.Provider", "ClassSchedule.Instructor", "ClassSchedule.Location", "StudentEvaluation.StudentEvaluationQuestions.QuestionBank", "StudentEvaluation.StudentEvaluationWithoutEmps" });
            return evaluationLinks.ToList();
        }
    }
}
