using QTD2.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core.Specifications.TrainingProgramReview_TrainingIssue_LinkSpecs
{
    public class TrainingProgramReview_TrainingIssue_LinkTrainingIssueIdSpec:ISpecification<TrainingProgramReview_TrainingIssue_Link>
    {
        public bool IsSatisfiedBy(TrainingProgramReview_TrainingIssue_Link entity, params object[] args)
        {
            return entity.TrainingIssueId > 0;
        }
    }
}
