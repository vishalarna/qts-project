using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.StudentEvaluation_QuestionSpecs
{
    public class StudentEvaluation_QuestionQuestionIdRequiredSpec : ISpecification<StudentEvaluation_Question>
    {
        public bool IsSatisfiedBy(StudentEvaluation_Question entity, params object[] args)
        {
            return entity.QuestionBankId > 0;
        }
    }
}
