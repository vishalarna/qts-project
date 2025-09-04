using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.StudentEvaluationQuestionSpecs
{
    public class StudentEvalQuestionNumberRequiredSpec : ISpecification<StudentEvaluationQuestion>
    {
        public bool IsSatisfiedBy(StudentEvaluationQuestion entity, params object[] args)
        {
            return entity.QuestionNumber > 0;
        }
    }
}
