using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.StudentEvaluationFormSpecs
{
    public class StudentEvalFormRatingScaleIdRequiredSpec : ISpecification<StudentEvaluationForm>
    {
        public bool IsSatisfiedBy(StudentEvaluationForm entity, params object[] args)
        {
            return entity.RatingScaleId > 0;
        }
    }
}
