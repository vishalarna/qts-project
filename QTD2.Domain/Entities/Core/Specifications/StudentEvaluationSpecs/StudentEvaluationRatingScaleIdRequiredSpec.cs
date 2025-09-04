using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.StudentEvaluationSpecs
{
    public class StudentEvaluationRatingScaleIdRequiredSpec : ISpecification<StudentEvaluation>
    {
        public bool IsSatisfiedBy(StudentEvaluation entity, params object[] args)
        {
            return entity.RatingScaleId > 0;
        }
    }
}
