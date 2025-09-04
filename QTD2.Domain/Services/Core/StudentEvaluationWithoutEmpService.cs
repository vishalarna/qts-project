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
    public class StudentEvaluationWithoutEmpService : Common.Service<StudentEvaluationWithoutEmp>, IStudentEvaluationWithoutEmpService
    {
        public StudentEvaluationWithoutEmpService(IStudentEvaluationWithoutEmpRepository repository, IStudentEvaluationWithoutEmpValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<StudentEvaluationWithoutEmp>> GetStudentEvaluationWithoutEmpByClassandEvalId(List<int> classscheduleIds, List<int> studentEvaluationIds)
        {
            List<Expression<Func<StudentEvaluationWithoutEmp, bool>>> predicates = new List<Expression<Func<StudentEvaluationWithoutEmp, bool>>>();
            predicates.Add(r => classscheduleIds.Contains(r.ClassScheduleId));
            predicates.Add(r => studentEvaluationIds.Contains(r.StudentEvaluationId));
            var result = await FindWithIncludeAsync(predicates, new string[] { "RatingScaleExpanded" });
            return result.ToList();
        }
    }
}
