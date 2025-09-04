using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.StudentEvaluationAudienceSpecs
{
    public class StudentEvaluationAudienceNameRequiredSpec : ISpecification<StudentEvaluationAudience>
    {
        public bool IsSatisfiedBy(StudentEvaluationAudience entity, params object[] args)
        {
            return !string.IsNullOrEmpty(entity.Name);
        }
    }
}
