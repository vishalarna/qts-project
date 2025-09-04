using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.TrainingProgramReview_Employee_LinkSpecs
{
    public class TrainingProgramReview_Employee_Link_TrainingProgramReviewIdRequiredSpec : ISpecification<TrainingProgramReview_Employee_Link>
    {
        public bool IsSatisfiedBy(TrainingProgramReview_Employee_Link entity, params object[] args)
        {
            return entity.TrainingProgramReviewId > 0;
        }
    }
}
