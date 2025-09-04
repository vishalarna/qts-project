using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Interfaces.Specification;

namespace QTD2.Domain.Entities.Core.Specifications.TrainingProgramReviewsSpecs
{
    public class TrainingProgramReviews_TrainingProgramIdRequiredSpec : ISpecification<TrainingProgramReview>
    {
        public bool IsSatisfiedBy(TrainingProgramReview entity, params object[] args)
        {
            return (entity.TrainingProgramId > 0);
        }
    }
}
